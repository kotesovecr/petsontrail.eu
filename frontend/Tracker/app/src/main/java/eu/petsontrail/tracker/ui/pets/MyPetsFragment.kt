package eu.petsontrail.tracker.ui.pets

import activities.ActivitiesGrpc
import android.os.Build
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.ListView
import androidx.annotation.RequiresApi
import eu.petsontrail.tracker.databinding.FragmentActivityBinding
import eu.petsontrail.tracker.databinding.FragmentMyPetsBinding
import eu.petsontrail.tracker.db.AppDatabase
import eu.petsontrail.tracker.db.DbHelper
import eu.petsontrail.tracker.dtos.MyPetDto
import eu.petsontrail.tracker.services.AuthenticationCallCredentials
import getmypets.GetMyPetsRequestOuterClass
import io.grpc.ManagedChannelBuilder
import kotlinx.coroutines.runBlocking
import pets.PetsGrpc
import java.util.UUID

class MyPetsFragment : Fragment() {
    private var _binding: FragmentMyPetsBinding? = null
    private val binding get() = _binding!!

    private lateinit var _db: AppDatabase

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        _db = DbHelper().InitializeDatabase(this.requireContext())
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        _binding = FragmentMyPetsBinding.inflate(inflater, container, false)

        return binding.root
    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val myPetsLists: ArrayList<MyPetDto> = ArrayList()

        runBlocking {
            val token = _db.userSettingsDao().getAccessToken()

            val channel = ManagedChannelBuilder
                .forTarget("dns:///petsontrail.eu:4443")
                .build()

            var client = PetsGrpc
                .newBlockingStub(channel)
                .withCallCredentials(AuthenticationCallCredentials(token))

            var response = client.getMyPets(GetMyPetsRequestOuterClass.GetMyPetsRequest.newBuilder().build())

            for (pet in response.petsList) {
                myPetsLists.add(MyPetDto(UUID.fromString(pet.id), pet.name, pet.chip))
            }
        }

        val adapter = MyPetsAdapter(this.requireContext(), myPetsLists)

        binding.mypetslist.adapter = adapter
    }
}