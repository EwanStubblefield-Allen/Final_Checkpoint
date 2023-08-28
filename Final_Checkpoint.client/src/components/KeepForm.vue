<template>
  <div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="keepForm" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-body">
          <div class="text-end">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <p v-if="!editable.id" class="fs-2 pb-3">Add your keep</p>
          <p v-else class="fs-2 pb-3">Edit your keep</p>

          <form @submit.prevent="handleSubmit()">
            <div class="pb-5">
              <div class="form-group">
                <label for="keepTitle" class="form-label"></label>
                <input v-model="editable.name" id="keepTitle" class="form-control underline" type="text" minlength="3" maxlength="255" placeholder="Title..." required>
              </div>

              <div class="form-group">
                <label for="keepImg" class="form-label"></label>
                <input v-model="editable.img" id="keepImg" class="form-control underline" type="url" minlength="3" maxlength="255" placeholder="Url..." required>
              </div>
            </div>

            <div class="pt-5">
              <div class="form-group">
                <label for="keepDescription" class="form-label"></label>
                <textarea v-model="editable.description" id="keepDescription" class="form-control underline" rows="1" minlength="3" maxlength="500" placeholder="Keep Description..." required></textarea>
              </div>

              <div class="text-end pt-5">
                <button v-if="!editable.id" type="submit" class="btn btn-dark">Create</button>
                <button v-else type="submit" class="btn btn-dark">Edit</button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, watchEffect } from 'vue'
import { keepsService } from '../services/KeepsService.js'
import { AppState } from '../AppState.js'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const editable = ref({})

    watchEffect(() => {
      if (AppState.keepEdit) {
        editable.value = { ...AppState.activeKeep }
      } else {
        editable.value = {}
      }
    })

    async function createKeep() {
      try {
        await keepsService.createKeep(editable.value)
        Pop.success(`${editable.value.name} was created!`)
      } catch (error) {
        Pop.error(error.message, '[CREATING KEEP]')
      }
    }

    async function updateKeep() {
      try {
        await keepsService.updateKeep(editable.value)
        Pop.success(`${editable.value.name} was updated!`)
      } catch (error) {
        Pop.error(error.message, '[UPDATING KEEP]')
      }
    }

    return {
      editable,

      handleSubmit() {
        if (AppState.keepEdit) {
          updateKeep()
        } else {
          createKeep()
        }
        Modal.getOrCreateInstance('#keepForm').hide()
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>