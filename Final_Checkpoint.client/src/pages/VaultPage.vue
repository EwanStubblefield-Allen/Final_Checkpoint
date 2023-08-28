<template>
  <div v-if="vault" class="col-10 col-md-5 py-3">
    <div class="position-relative">
      <img class="rounded w-100" :src="vault.img" :alt="vault.name">
      <section class="row">
        <div class="col-10 card-name text-center p-2">
          <p class="fs-1">{{ vault.name }}</p>
          <p>by {{ vault.creator.name }}</p>
        </div>
      </section>
    </div>

    <div class="pt-2">
      <div v-if="vault.creator.id == account.id" class="dropdown text-end">
        <button type="button" class="btn selectable no-select mdi mdi-dots-horizontal fs-3 py-0 px-2" data-bs-toggle="dropdown" aria-expanded="false" title="More Options"></button>

        <div class="dropdown-menu dropdown-menu-end p-0" aria-labelledby="vaultDropdown">
          <div class="list-group text-center">
            <div @click="isEditing()" class="list-group-item dropdown-item list-group-item-action selectable" data-bs-toggle="modal" data-bs-target="#vaultForm">
              <p class="mdi mdi-pencil">Edit Vault</p>
            </div>

            <div @click="removeVault()" class="list-group-item dropdown-item list-group-item-action text-danger selectable">
              <p class="mdi mdi-trash-can">Delete Vault</p>
            </div>
          </div>
        </div>
      </div>

      <div class="d-flex justify-content-center">
        <p class="count p-2">{{ keeps.length }} Keeps</p>
      </div>
    </div>
  </div>

  <div class="col-12 col-md-10">
    <div class="masonry">
      <div v-for="k in keeps" :key="k.id" class="masonry-item my-2">
        <KeepCard :keepProp="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { useRoute, useRouter } from 'vue-router'
import { computed, onUnmounted, watchEffect } from 'vue'
import { AppState } from '../AppState.js'
import { vaultsService } from '../services/VaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()

    watchEffect(() => {
      getVaultById(route.params.vaultId)
      getVaultKeepsByVaultId(route.params.vaultId)
    })

    onUnmounted(() => {
      AppState.activeVault = null
      AppState.keeps = []
    })

    async function getVaultById(vaultId) {
      try {
        await vaultsService.getVaultById(vaultId)
      } catch (error) {
        Pop.error(error.message, '[GETTING VAULT BY ID]')
        router.push('/')
      }
    }

    async function getVaultKeepsByVaultId(vaultId) {
      try {
        await vaultKeepsService.getVaultKeepsByVaultId(vaultId)
      } catch (error) {
        Pop.error(error.message, '[GETTING VAULT KEEPS BY VAULT ID]')
      }
    }

    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),

      isEditing() {
        AppState.vaultEdit = true
      },

      async removeVault() {
        try {
          const isSure = await Pop.confirm(`Are you sure you want to delete ${this.vault.name}?`)

          if (!isSure) {
            return
          }
          await vaultsService.removeVault(this.vault.id)
          router.push('/')
          Pop.toast('Vault was deleted!')
        } catch (error) {
          Pop.error(error.message, '[DELETING Vault]')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
  img {
    height: 30vh;
    object-fit: cover;
    object-position: center;
  }

  button {
    line-height: normal;
  }

  .count {
    border-radius: 15px;
    background: #DED6E9;
  }

  .card-name {
    position: absolute;
    left: 8%;
    bottom: 0;
  }
</style>