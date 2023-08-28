<template>
  <div class="modal fade" id="accountForm" tabindex="-1" aria-labelledby="accountForm" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-body">
          <div class="text-end">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <p class="fs-2 pb-3">Edit your profile</p>

          <form @submit.prevent="updateAccount()">
            <div class="pb-5">
              <div class="form-group">
                <label for="accountName">Username</label>
                <input v-model="editable.name" id="accountName" class="form-control underline" type="text" minlength="3" maxlength="255" placeholder="Username..." required>
              </div>

              <div class="form-group pt-3">
                <label for="accountPicture">Profile Picture</label>
                <input v-model="editable.picture" id="accountPicture" class="form-control underline" type="url" minlength="3" maxlength="255" placeholder="Url..." required>
              </div>
            </div>

            <div class="form-group">
              <label for="accountCoverImg">Cover Image</label>
              <input v-model="editable.coverImg" id="accountCoverImg" class="form-control underline" type="url" minlength="3" maxlength="255" placeholder="Url..." required>
            </div>

            <div class="text-end pt-5">
              <button type="submit" class="btn btn-dark">Update</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, watchEffect } from 'vue'
import { AppState } from '../AppState.js'
import { accountService } from '../services/AccountService.js'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const editable = ref({})

    watchEffect(() => {
      editable.value = { ...AppState.account }
    })

    return {
      editable,

      async updateAccount() {
        try {
          await accountService.updateAccount(editable.value)
          Modal.getOrCreateInstance('#accountForm').hide()
          Pop.success('Profile Updated!')
        } catch (error) {
          Pop.error(error.message, '[UPDATING ACCOUNT]')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>