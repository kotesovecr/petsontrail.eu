package eu.petsontrail.tracker.db.dao

import androidx.lifecycle.LiveData
import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.Query
import androidx.room.Update
import eu.petsontrail.tracker.db.model.ActivityDto

@Dao
interface ActivityDao {
    @Query("SELECT * FROM my_activities")
    suspend fun getAll(): List<ActivityDto>

    @Query("SELECT * FROM my_activities WHERE active=1 LIMIT 1")
    fun getLiveActive(): LiveData<ActivityDto?>

    @Query("SELECT * FROM my_activities WHERE active=1 LIMIT 1")
    fun getActiveNoSuspend(): ActivityDto?

    @Query("SELECT * FROM my_activities WHERE active=1 LIMIT 1")
    suspend fun getActive(): ActivityDto?

    @Query("SELECT * FROM my_activities WHERE uid IN (:activityIds)")
    suspend fun loadAllByIds(activityIds: IntArray): List<ActivityDto>

    // @Query("SELECT * FROM my_activities WHERE synchronize = true")
    @Query("SELECT * FROM my_activities")
    suspend fun getActivitiesToSynchronize(): List<ActivityDto>

    @Query("SELECT * FROM my_activities WHERE name LIKE :name LIMIT 1")
    suspend fun findByName(name: String): ActivityDto?

    @Query("UPDATE my_activities SET active=0 WHERE active=1")
    suspend fun resetActiveActivities()

    @Insert
    suspend fun insertOne(vararg activity: ActivityDto)

    @Update(ActivityDto::class)
    suspend fun update(obj: ActivityDto)

    @Delete
    suspend fun delete(activity: ActivityDto)
}