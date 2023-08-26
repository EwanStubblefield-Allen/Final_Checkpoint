<template>
  <div class="col-12 col-md-8 mt-5 position-relative">
    <img class="cover-img" :src="profile.coverImg" :alt="`${profile.name} cover image`">
    <img class="profile-pic elevation-4" :src="profile.picture" :alt="profile.name">
  </div>
  <div class="col-12 text-center">
    <p class="fs-1 fw-bold mt-5">{{ profile.name }}</p>
    <div class="d-flex justify-content-center">
      <p>{{ vaults.length }} Vaults</p>
      <div class="vr mx-2 opacity-100"></div>
      <p>{{ keeps.length }} Keeps</p>
    </div>
  </div>
  <div class="col-12 col-md-10">
    <div class="my-2">
      <p class="fs-2 fw-bold">Vaults</p>
      <section class="row">
        <div v-for="v in vaults" :key="v.id" class="col-12 col-sm-6 col-md-4 col-lg-3 p-3">
          <VaultCard :vaultProp="v" />
        </div>
      </section>
    </div>

    <div class="my-2">
      <p class="fs-2 fw-bold">Keeps</p>
      <div class="masonry my-2">
        <div v-for="k in keeps" :key="k.id" class="masonry-item my-2">
          <KeepCard :keepProp="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useRoute } from 'vue-router'
import { computed, watchEffect } from 'vue'
import { profilesService } from '../services/ProfilesService.js'
import { keepsService } from '../services/KeepsService.js'
import { vaultsService } from '../services/VaultsService.js'
import { AppState } from '../AppState.js'
import KeepCard from '../components/KeepCard.vue'
import VaultCard from '../components/VaultCard.vue'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const route = useRoute()
    watchEffect(() => {
      getProfile(route.params.profileId)
      getKeepsByProfileId(route.params.profileId)
      getVaultsByProfileId(route.params.profileId)
    })

    async function getProfile(profileId) {
      try {
        await profilesService.getProfile(profileId)
      }
      catch (error) {
        Pop.error(error.message, '[GETTING PROFILE]')
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

    async function getVaultsByProfileId(profileId) {
      try {
        await vaultsService.getVaultsByProfileId(profileId)
      }
      catch (error) {
        Pop.error(error.message, '[GETTING VAULTS BY PROFILE ID]')
      }
    }
    return {
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { KeepCard, VaultCard }
}
</script>

<style lang="scss" scoped>
  .cover-img {
    height: 50vh;
    width: 100%;
    object-fit: cover;
    object-position: center;
  }

  .profile-pic {
    height: 6rem;
    width: 6rem;
    border: 2px solid white;
    border-radius: 50%;
    position: absolute;
    left: calc(50% - 3rem);
    bottom: -3rem;
  }
</style>