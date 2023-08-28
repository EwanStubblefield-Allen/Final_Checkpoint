<template>
  <div class="modal fade" id="vaultForm" tabindex="-1" aria-labelledby="vaultForm" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-body">
          <div class="text-end">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <p v-if="!editable.id" class="fs-2">Add your vault</p>
          <p v-else class="fs-2">Edit your vault</p>

          <form @submit.prevent="handleSubmit()">
            <div class="py-3">
              <div class="form-group">
                <label for="vaultTitle" class="form-label"></label>
                <input v-model="editable.name" id="vaultTitle" class="form-control underline" type="text" minlength="3" maxlength="255" placeholder="Title..." required>
              </div>

              <div class="form-group">
                <label for="vaultImg" class="form-label"></label>
                <input v-model="editable.img" id="vaultImg" class="form-control underline" type="url" minlength="3" maxlength="255" placeholder="Image Url..." required>
              </div>

              <div class="form-group">
                <label for="vaultDescription" class="form-label"></label>
                <textarea v-model="editable.description" id="vaultDescription" class="form-control underline" rows="1" minlength="3" maxlength="500" placeholder="Vault Description..." required></textarea>
              </div>
            </div>

            <div class="form-group text-end py-3">
              <div class="form-text">
                Private Vaults can only be seen by you.
              </div>
              <input v-model="editable.isPrivate" id="vaultIsPrivate" class="form-check-input" type="checkbox">
              <label for="vaultIsPrivate" class="form-check-label px-2">Make Vault Private?</label>
            </div>

            <div class="text-end">
              <button v-if="!editable.id" type="submit" class="btn btn-dark px-5">Create Vault</button>
              <button v-else type="submit" class="btn btn-dark px-5">Edit Vault</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { router } from '../router.js'
import { ref, watchEffect } from 'vue'
import { AppState } from '../AppState.js'
import { vaultsService } from '../services/VaultsService.js'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const editable = ref({})

    watchEffect(() => {
      if (AppState.vaultEdit) {
        editable.value = { ...AppState.activeVault }
      } else {
        editable.value = {}
      }
    })

    async function createVault() {
      try {
        const vaultId = await vaultsService.createVault(editable.value)
        router.push({ name: 'Vault', params: { vaultId: vaultId } })
        Pop.success(`${editable.value.name} was created!`)
        editable.value = {}
      } catch (error) {
        Pop.error(error.message, '[CREATING VAULT]')
      }
    }

    async function updateVault() {
      try {
        await vaultsService.updateVault(editable.value)
        Pop.success(`${editable.value.name} was updated!`)
        editable.value = {}
      } catch (error) {
        Pop.error(error.message, '[UPDATING VAULT]')
      }
    }

    return {
      editable,

      handleSubmit() {
        if (AppState.vaultEdit) {
          updateVault()
        } else {
          createVault()
        }
        Modal.getOrCreateInstance('#vaultForm').hide()
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>