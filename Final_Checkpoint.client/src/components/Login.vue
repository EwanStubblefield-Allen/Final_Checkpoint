<template>
  <button class="d-flex justify-content-end btn selectable text-uppercase" @click="login" v-if="!user.isAuthenticated">
    Login
  </button>
  <div v-else>
    <div :class="{ 'd-none': width >= 768 }" class="d-flex justify-content-end dropdown">
      <div type="button" role="button" class="border-0 selectable no-select rounded-circle elevation-4" data-bs-toggle="dropdown" aria-expanded="false">
        <div v-if="account.picture || user.picture">
          <img :src="account.picture || user.picture" alt="account photo" height="40" class="rounded-circle" />
        </div>
      </div>
      <div class="dropdown-menu dropdown-menu-end p-0" aria-labelledby="authDropdown">
        <div class="list-group">
          <router-link :to="{ name: 'Account' }">
            <div class="list-group-item dropdown-item list-group-item-action">
              Manage Account
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
      width: computed(() => window.innerWidth),

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