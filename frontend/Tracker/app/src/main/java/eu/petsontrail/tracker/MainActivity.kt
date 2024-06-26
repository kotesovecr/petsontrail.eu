package eu.petsontrail.tracker

import android.content.Intent
import android.os.Build
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.navigateUp
import androidx.navigation.ui.setupActionBarWithNavController
import android.view.Menu
import android.view.MenuItem
import android.widget.Button
import androidx.annotation.RequiresApi
import eu.petsontrail.tracker.databinding.ActivityMainBinding
import eu.petsontrail.tracker.db.AppDatabase
import eu.petsontrail.tracker.db.model.UserSettingsDto
import eu.petsontrail.tracker.services.ActivityUploadService
import io.grpc.ManagedChannelBuilder
import kotlinx.coroutines.runBlocking
import java.util.UUID

class MainActivity : AppCompatActivity() {

    private lateinit var appBarConfiguration: AppBarConfiguration
    private lateinit var binding: ActivityMainBinding

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        var channel = ManagedChannelBuilder.forTarget("dns:///petsontrail.eu:4443").build()
        var state = channel.getState(true)

        Log.d("Service status", state.toString())

//        var actionsClient = ActionsGrpc.newBlockingStub(channel)

//        var publicActions = actionsClient.getPublicActionsList(Empty.newBuilder().build())
//        Log.d("public actions", publicActions.toString())


        runBlocking {
            val userSettingsDao = AppDatabase.getDatabase(this@MainActivity, this).userSettingsDao()
            if (userSettingsDao.getAll().size == 0) {
                val userSettings = UserSettingsDto(UUID.randomUUID(), null, null, null, null, null, "")
                userSettingsDao.insertOne(userSettings)
            }
        }


        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setSupportActionBar(binding.toolbar)

        val navController = findNavController(R.id.nav_host_fragment_content_main)
        appBarConfiguration = AppBarConfiguration(navController.graph)
        setupActionBarWithNavController(navController, appBarConfiguration)

        /*
        binding.fab.setOnClickListener { view ->
            Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                .setAction("Action", null)
                .setAnchorView(R.id.fab).show()
        }
        */

        val btnLogin: Button = binding.btnLogin
        btnLogin.setOnClickListener {
            val intent = Intent(applicationContext, LoginActivity::class.java)
            startActivity(intent)
        }

        binding.btnSynchronize.setOnClickListener {
            val intent = Intent(applicationContext, ActivityUploadService::class.java)
            applicationContext.startForegroundService(intent)
        }
    }

    override fun onCreateOptionsMenu(menu: Menu): Boolean {
        // Inflate the menu; this adds items to the action bar if it is present.
        menuInflater.inflate(R.menu.menu_main, menu)
        return true
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        return when (item.itemId) {
            R.id.action_settings -> true
            else -> super.onOptionsItemSelected(item)
        }
    }

    override fun onSupportNavigateUp(): Boolean {
        val navController = findNavController(R.id.nav_host_fragment_content_main)
        return navController.navigateUp(appBarConfiguration)
                || super.onSupportNavigateUp()
    }
}