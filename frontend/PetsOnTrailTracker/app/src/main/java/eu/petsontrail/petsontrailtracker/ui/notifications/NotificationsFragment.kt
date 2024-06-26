package eu.petsontrail.petsontrailtracker.ui.notifications

import android.content.Intent
import android.os.Build
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.annotation.RequiresApi
import androidx.credentials.CredentialManager
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import eu.petsontrail.petsontrailtracker.ActivityUploadService
import eu.petsontrail.petsontrailtracker.LocationTrackerService
import eu.petsontrail.petsontrailtracker.databinding.FragmentNotificationsBinding
import eu.petsontrail.petsontrailtracker.ui.login.LoginActivity
import net.openid.appauth.AuthorizationException
import net.openid.appauth.AuthorizationResponse


class NotificationsFragment : Fragment() {
    private var RC_AUTH: Int = 11122
    private var TAG: String = "User settings"

    private var _binding: FragmentNotificationsBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    private lateinit var credentialManager: CredentialManager

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {
        val notificationsViewModel =
                ViewModelProvider(this).get(NotificationsViewModel::class.java)

        _binding = FragmentNotificationsBinding.inflate(inflater, container, false)
        val root: View = binding.root
        var context = this.requireContext()

        val btnLogin: Button = binding.btnUserLogin
        btnLogin.setOnClickListener {
            val intent = Intent(context, LoginActivity::class.java)
            startActivity(intent)
        }

        val btnSynchronize: Button = binding.btnSynchronize
        btnSynchronize.setOnClickListener {
            val intent = Intent(this.context, ActivityUploadService::class.java)
            this.context?.startForegroundService(intent)
        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        if (requestCode == RC_AUTH) {
            val resp = AuthorizationResponse.fromIntent(data!!)
            val ex = AuthorizationException.fromIntent(data)

            // … process the response or exception …
        } else {

            // …
        }
    }
}