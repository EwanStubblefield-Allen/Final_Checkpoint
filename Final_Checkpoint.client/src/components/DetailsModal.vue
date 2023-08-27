<template>
  <div class="modal fade" id="detailsModal" tabindex="-1" aria-labelledby="detailsModal" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="container-fluid modal-content">
        <div v-if="keep" class="row">
          <div class="col-12 col-lg-6 p-0">
            <img class="keep-img rounded" :src="keep.img" :alt="keep.name">
          </div>

          <div class="col-12 col-lg-6 d-flex flex-column justify-content-between text p-4">
            <section class="row">
              <div class="offset-2 col-8 d-flex justify-content-center align-items-center">
                <i class="mdi mdi-eye-outline fs-5"></i>
                <p class="px-2">{{ keep.views }}</p>
                <p class="border border-dark rounded mx-2 px-1">k</p>
                <p>{{ keep.kept }}</p>
              </div>
              <div class="col-2 d-flex justify-content-end align-items-center">
                <i v-if="keep.creatorId == account.id" @click="keepEdit()" class="mdi mdi-pencil selectable fs-5 mx-2" data-bs-toggle="modal" data-bs-target="#keepForm" title="Edit Keep"></i>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
            </section>

            <div>
              <p class="fs-1 fw-bold text-center">{{ keep.name }}</p>
              <p>{{ keep.description }}</p>
            </div>

            <div class="d-flex justify-content-between">
              <div @click="removeVaultKeep()" v-if="keep.vaultKeepId && keep.creatorId == account.id" class="d-flex align-items-center underline text-secondary selectable">
                <i class="mdi mdi-cancel pe-2"></i>
                <p>Remove</p>
              </div>

              <form @submit.prevent="createVaultKeep()" v-else-if="account.id" class="d-flex align-items-center">
                <select v-model="editable.vaultId" class="form-select" aria-label="Select vault" required>
                  <option selected>Vault</option>
                  <option v-for="v in vaults" :key="v.id" :value="v.id">{{ v.name }}</option>
                </select>
                <button class="d-none d-md-block btn btn-dark text-light mx-3" type="submit">Save</button>
              </form>
              <div v-else></div>

              <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
                <div class="d-flex justify-content-end align-items-center truncate" data-bs-dismiss="modal">
                  <img class="profile-pic mx-2" :src="keep.creator.picture" :alt="keep.creator.name">
                  <p class="text-dark text-truncate">{{ keep.creator.name }}</p>
                </div>
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useRouter } from 'vue-router'
import { computed, ref } from 'vue'
import { AppState } from '../AppState.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const router = useRouter()
    const editable = ref({ vaultId: 'Vault' })

    return {
      editable,
      account: computed(() => AppState.account),
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),

      keepEdit() {
        AppState.keepEdit = true
      },

      async createVaultKeep() {
        try {
          if (editable.value.vaultId == 'Vault') {
            return
          }
          await vaultKeepsService.createVaultKeep(editable.value)
          Modal.getOrCreateInstance('#detailsModal').hide()
          router.push({ name: 'Vault', params: { vaultId: editable.value.vaultId } })
          editable.value.vaultId = 'Vault'
          Pop.success(`${this.keep.name} was saved!`)
        } catch (error) {
          Pop.error(error.message, '[CREATING VAULT KEEP]')
        }
      },

      async removeVaultKeep() {
        try {
          const isSure = await Pop.confirm(`Are you sure you want to delete ${this.keep.name}?`)

          if (!isSure) {
            return
          }
          vaultKeepsService.removeVaultKeep(this.keep.vaultKeepId)
          Modal.getOrCreateInstance('#detailsModal').hide()
          Pop.toast(`Keep was removed!`)
        } catch (error) {
          Pop.error(error.message, '[DELETING VAULT KEEP]')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
  form button {
    border: none;
    border-radius: 10px;
    background: #877A8F;
    box-shadow: 0px 4px 4px 0px rgba(162, 119, 217, 0.25);
  }

  .keep-img {
    width: 100%;
    height: 75vh;
    object-fit: cover;
    object-position: center;
  }

  .text {
    min-height: 60vh;
  }

  .profile-pic {
    height: 2rem;
    width: 2rem;
    border-radius: 50%;
  }

  .truncate {
    max-width: 50vw;
    pointer-events: all;
  }
</style>