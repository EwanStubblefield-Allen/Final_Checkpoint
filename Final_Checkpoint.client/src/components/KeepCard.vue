<template>
  <div class="d-flex align-items-end bg-img rounded elevation-4 h-100 p-3">
    <div class="d-flex justify-content-between align-items-center w-100">
      <p class="card-name fs-4">{{ keepProp.name }}</p>
      <router-link :to="{ name: 'Profile', params: { profileId: keepProp.creator.id } }" v-if="!profile.id">
        <img class="profile-pic" :src="keepProp.creator.picture" :alt="keepProp.creator.name" :title="keepProp.creator.name">
      </router-link>
    </div>
  </div>
</template>

<script>
import { computed, onUnmounted } from 'vue'
import { Keep } from '../models/Keep.js'
import { AppState } from '../AppState.js'

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
      backgroundImg: computed(() => `url("${props.keepProp.img}")`)
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