<template>
  <button class="btn selectable text-uppercase" @click="login" v-if="!user.isAuthenticated">
    Login
  </button>
  <div v-else>
    <div class="dropdown">
      <div type="button" role="button" class="border-0 selectable no-select rounded-circle elevation-4" data-bs-toggle="dropdown" aria-expanded="false">
        <div v-if="account.picture || user.picture">
          <img :src="account.picture || user.picture" alt="account photo" style="height: 40px; width: 40px;" class="rounded-circle" />
        </div>
      </div>
      <div class="dropdown-menu dropdown-menu-end p-0" aria-labelledby="authDropdown">
        <div v-if="account.id" class="list-group">
          <router-link :to="{ name: 'Account' }">
            <div class="list-group-item dropdown-item list-group-item-action">
              Manage Account
            </div>
          </router-link>
          <router-link :to="{ name: 'Profile', params: { profileId: account.id } }">
            <div class="list-group-item dropdown-item list-group-item-action">
              My Page
            </div>
          </router-link>
          <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
            <i class="mdi mdi-logout"></i>
            logout
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'

export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),

      async login() {
        AuthService.loginWithPopup()
      },

      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>