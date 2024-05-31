package eu.petsontrail.tracker.dtos

import java.util.UUID

class PetTypeDto(val id: UUID, val name: String, val description: String) {
    override fun toString(): String {
        return name
    }
}
