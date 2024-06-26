package eu.petsontrail.petsontrailtracker

import android.location.Location
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import androidx.room.Room
import com.google.android.material.bottomnavigation.BottomNavigationView
import eu.petsontrail.petsontrailtracker.data.AppDatabase
import eu.petsontrail.petsontrailtracker.data.UserSettingsDto
import eu.petsontrail.petsontrailtracker.databinding.ActivityMainBinding
import eu.petsontrail.petsontrailtracker.helper.DbHelper
import kotlinx.coroutines.runBlocking
import java.util.UUID


class MainActivity : AppCompatActivity(), LocationUpdateListener {

    private lateinit var binding: ActivityMainBinding

    private lateinit var locations: MutableList<Location>

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val db = DbHelper().InitializeDatabase(applicationContext)

        runBlocking {
            if (db.userSettingsDao().getAll().size == 0) {
                var userSettings = UserSettingsDto(UUID.randomUUID(), null, null, null, null, null, "")
                db.userSettingsDao().insertOne(userSettings)
            }
        }

        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val navView: BottomNavigationView = binding.navView

        val navHostFragment = supportFragmentManager.findFragmentById(R.id.nav_host_fragment_activity_main) as NavHostFragment
        val navController = navHostFragment.navController

//         val navController = findNavController(R.id.nav_host_fragment_activity_main)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        val appBarConfiguration = AppBarConfiguration(setOf(
                R.id.navigation_home, R.id.navigation_activities, R.id.navigation_notifications))
        setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)
    }

    override fun onUpdateLocation(location: Location): Int {
        locations.add(location)

        return 1
    }

    override fun onUpdateMultipleLocations(newLocations: MutableList<Location>): Int {
        locations.addAll(newLocations)

        return newLocations.size
    }
}