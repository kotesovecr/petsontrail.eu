package eu.petsontrail.tracker

import android.Manifest
import android.content.Intent
import android.content.pm.PackageManager
import android.os.Build
import android.os.Bundle
import android.text.format.DateUtils
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.annotation.RequiresApi
import androidx.core.app.ActivityCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import eu.petsontrail.tracker.databinding.FragmentActivityBinding
import eu.petsontrail.tracker.db.AppDatabase
import eu.petsontrail.tracker.helpers.DistanceHelper
import eu.petsontrail.tracker.services.LocationTrackerService
import eu.petsontrail.tracker.viewmodels.ActivityViewModel
import eu.petsontrail.tracker.viewmodels.LocationViewModel
import kotlinx.coroutines.invoke
import kotlinx.coroutines.runBlocking
import java.lang.StringBuilder
import java.time.LocalDateTime
import java.time.ZoneOffset
import java.util.UUID

class ActivityFragment : Fragment() {
    private var _binding: FragmentActivityBinding? = null
    private val binding get() = _binding!!

    private val REQUEST_PERMISSION_LOCATIONS = 1;
    private var _activityId: UUID? = null
    private var _duration = 0L

    private lateinit var _locationViewModel: LocationViewModel
    private lateinit var _activityViewModel: ActivityViewModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        _binding = FragmentActivityBinding.inflate(inflater, container, false)
        _locationViewModel = ViewModelProvider(this).get(LocationViewModel::class.java)
        _locationViewModel.setActivityId(_activityId)
        _locationViewModel.locations.observe(viewLifecycleOwner, Observer { locations ->
            Log.d("location", "location updated")

            var distance = 0.0
            var speed = 0.0

            if (locations.size > 1) {
                distance = DistanceHelper().GetDistanceInMeters(locations) / 1000
                speed = distance / _duration * 3600 / 1000
            }

            binding.textViewActivityDistance.text = distance.toString()
            binding.textViewActivitySpeed.text = speed.toString()
        })

        _activityViewModel = ViewModelProvider(this).get(ActivityViewModel::class.java)
        _activityViewModel.activity.observe(viewLifecycleOwner, Observer { activity ->
            if (activity != null) {
                binding.textViewActivityName.text = activity.name

                if (activity.start != null) {
                    val now = LocalDateTime.now().toEpochSecond(ZoneOffset.UTC)
                    _duration = now - activity.start!!
                }

                val timeString = DateUtils.formatElapsedTime(StringBuilder(""), _duration)
                binding.textViewActivityDuration.text = timeString
            }
        })

        if (ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED
            || ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED
            || ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.ACCESS_BACKGROUND_LOCATION) != PackageManager.PERMISSION_GRANTED
            || ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.FOREGROUND_SERVICE) != PackageManager.PERMISSION_GRANTED
            || ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.FOREGROUND_SERVICE_LOCATION) != PackageManager.PERMISSION_GRANTED
            || ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.INTERNET) != PackageManager.PERMISSION_GRANTED
            || ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.POST_NOTIFICATIONS) != PackageManager.PERMISSION_GRANTED
            || ActivityCompat.checkSelfPermission(this.requireContext(), Manifest.permission.FOREGROUND_SERVICE_DATA_SYNC) != PackageManager.PERMISSION_GRANTED
        ) {
            Log.d("permission", "no permission for locations is configured")

            val requestedPermissions = arrayOf(
                Manifest.permission.ACCESS_FINE_LOCATION,
                Manifest.permission.ACCESS_COARSE_LOCATION,
                Manifest.permission.ACCESS_BACKGROUND_LOCATION,
                Manifest.permission.FOREGROUND_SERVICE,
                Manifest.permission.FOREGROUND_SERVICE_LOCATION,
                Manifest.permission.INTERNET,
                Manifest.permission.POST_NOTIFICATIONS,
                Manifest.permission.FOREGROUND_SERVICE_DATA_SYNC)

            ActivityCompat.requestPermissions(
                this.requireActivity(),
                requestedPermissions,
                REQUEST_PERMISSION_LOCATIONS)
        }

        return binding.root
    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.buttonCreateNewActivity.setOnClickListener {
            findNavController().navigate(R.id.action_activityFragment_to_createActivityFragment)
        }

        binding.buttonActivityStart.setOnClickListener {
            runBlocking {
                _activityId = AppDatabase.getDatabase(requireContext(), lifecycleScope).activityDao().getActive()?.uid
                _locationViewModel.setActivityId(_activityId)

                val activity = AppDatabase.getDatabase(requireContext(), lifecycleScope).activityDao().getActive()
                if (activity != null) {
                    activity.start = LocalDateTime.now().toEpochSecond(ZoneOffset.UTC)
                    AppDatabase.getDatabase(requireContext(), lifecycleScope).activityDao().update(activity)
                }
            }

            val intent = Intent(this.context, LocationTrackerService::class.java)
            this.context?.startForegroundService(intent)
        }

        binding.buttonActivityStop.setOnClickListener {
            val intent = Intent(this.context, LocationTrackerService::class.java)
            this.context?.stopService(intent)

            runBlocking {
                val activity = AppDatabase.getActivityDao(requireContext(), lifecycleScope).getActive()
                if (activity != null) {
                    activity.end = LocalDateTime.now().toEpochSecond(ZoneOffset.UTC)
                    AppDatabase.getDatabase(requireContext(), lifecycleScope).activityDao().update(activity)
                }

                AppDatabase.getActivityDao(requireContext(), lifecycleScope).resetActiveActivities()
                _activityId = null
                _locationViewModel.setActivityId(_activityId)

                binding.textViewActivityName.text = ""
                binding.textViewActivityDuration.text = ""
                binding.textViewActivityDistance.text = ""
                binding.textViewActivitySpeed.text = ""
            }
        }
    }
}