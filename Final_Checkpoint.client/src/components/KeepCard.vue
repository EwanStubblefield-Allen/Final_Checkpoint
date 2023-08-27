<template>
  <div @click="getKeepById()" class="d-flex align-items-end bg-img selectable rounded elevation-4 h-100 p-3">
    <div class="d-flex justify-content-between align-items-center">
      <p class="card-name fs-4">{{ keepProp.name }}</p>
      <router-link :to="{ name: 'Profile', params: { profileId: keepProp.creator.id } }" v-if="!profile.id" @click.stop="">
        <img class="profile-pic" :src="keepProp.creator.picture" :alt="keepProp.creator.name" :title="keepProp.creator.name">
      </router-link>
    </div>
  </div>
</template>

<script>
import { computed, onUnmounted } from 'vue'
import { Keep } from '../models/Keep.js'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop.js'

export default {
  props: {
    keepProp: {
      type: Keep,
      required: true
    }
  },

  setup(props) {
    onUnmounted(() => {
      AppState.profile = {}
      AppState.vaults = []
      AppState.keeps = []
    })

    return {
      profile: computed(() => AppState.profile),
      backgroundImg: computed(() => `url("${props.keepProp.img}")`),

      getKeepById() {
        try {
          keepsService.getKeepById(props.keepProp)
          Modal.getOrCreateInstance('#detailsModal').show()
        } catch (error) {
          Pop.error(error.message, '[GETTING KEEP BY ID]')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
  .bg-img {
    background-image: v-bind(backgroundImg);
    background-position: center;
    background-size: cover;
  }

  .profile-pic {
    height: 3rem;
    width: 3rem;
    border-radius: 50%;
  }
</style>