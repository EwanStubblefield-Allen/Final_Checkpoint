<template>
  <div v-if="profile.id" class="col-12 col-md-8 mt-5 position-relative">
    <img class="cover-img rounded" :src="profile.coverImg" :alt="`${profile.name} cover image`">
    <img class="account-pic elevation-4" :src="profile.picture" :alt="profile.name">
  </div>

  <div v-if="profile.id" class="col-12 text-center">
    <p class="text-truncate max-vw-100 fs-1 fw-bold mt-5">{{ profile.name }}</p>
    <div class="d-flex justify-content-center">
      <p>{{ vaults.length }} Vaults</p>
      <div class="vr mx-2 opacity-100"></div>
      <p>{{ keeps.length }} Keeps</p>
    </div>
  </div>

  <div v-if="vaults.length" class="col-12 col-md-10">
    <div class="my-2">
      <p class="fs-2 fw-bold">Vaults</p>
      <section class="row">
        <div v-for="v in vaults" :key="v.id" class="col-12 col-sm-6 col-md-4 col-lg-3 p-3">
          <VaultCard :vaultProp="v" />
        </div>
      </section>
    </div>

    <div v-if="keeps.length" class="my-2">
      <p class="fs-2 fw-bold">Keeps</p>
      <div class="masonry my-2">
        <div v-for="k in keeps" :key="k.id" class="masonry-item my-2">
          <!-- FIXME Turn back into component -->
          <div @click="getKeepById()" class="d-flex align-items-end bg-img selectable rounded elevation-4 h-100 p-3">
            <div class="d-flex justify-content-between align-items-center">
              <p class="card-name fs-4">{{ k.name }}</p>
              <router-link :to="{ name: 'Profile', params: { profileId: k.creator.id } }" v-if="!profile.id" @click.stop="">
                <img class="profile-pic" :src="k.creator.picture" :alt="k.creator.name" :title="k.creator.name">
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useRoute } from 'vue-router'
import { computed, onUnmounted, watchEffect } from 'vue'
import { profilesService } from '../services/ProfilesService.js'
import { keepsService } from '../services/KeepsService.js'
import { vaultsService } from '../services/VaultsService.js'
import { AppState } from '../AppState.js'
import VaultCard from '../components/VaultCard.vue'
import KeepCard from '../components/KeepCard.vue'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const route = useRoute()

    watchEffect(() => {
      const profileId = route.params.profileId
      getProfile(profileId)
      getVaultsByProfileId(profileId)
      getKeepsByProfileId(profileId)
    })

    onUnmounted(() => {
      AppState.profile = {}
    })

    async function getProfile(profileId) {
      try {
        await profilesService.getProfile(profileId)
      }
      catch (error) {
        Pop.error(error.message, '[GETTING PROFILE]')
      }
    }

    async function getVaultsByProfileId(profileId) {
      try {
        await vaultsService.getVaultsByProfileId(profileId)
      }
      catch (error) {
        Pop.error(error.message, '[GETTING VAULTS BY PROFILE ID]')
      }
    }

    async function getKeepsByProfileId(profileId) {
      try {
        await keepsService.getKeepsByProfileId(profileId)
      }
      catch (error) {
        Pop.error(error.message, '[GETTING KEEPS BY PROFILE ID]')
      }
    }

    return {
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { VaultCard, KeepCard }
}
</script>

<style lang="scss" scoped>
  .cover-img {
    height: 50vh;
    width: 100%;
    object-fit: cover;
    object-position: center;
  }

  .account-pic {
    height: 6rem;
    width: 6rem;
    border: 2px solid white;
    border-radius: 50%;
    position: absolute;
    left: calc(50% - 3rem);
    bottom: -3rem;
  }
</style>