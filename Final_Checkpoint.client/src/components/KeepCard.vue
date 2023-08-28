<template>
  <div @click="getKeepById()" class="d-flex align-items-end bg-img position-relative selectable rounded elevation-4 p-3">
    <div class="d-flex justify-content-between align-items-center">
      <p class="card-name fs-4 text-truncate text-wrap">{{ keepProp.name }}</p>
      <router-link :to="{ name: 'Profile', params: { profileId: keepProp.creator.id } }" v-if="!profile.id" @click.stop="">
        <img class="profile-pic" :src="keepProp.creator.picture" :alt="keepProp.creator.name" :title="keepProp.creator.name">
      </router-link>
    </div>

    <i v-if="keepProp.creatorId == account.id" @click.stop="removeKeep()" class="mdi mdi-close d-flex justify-content-center align-items-center" title="Delete Keep"></i>
  </div>
</template>

<script>
import { computed } from 'vue'
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
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      backgroundImg: computed(() => `url("${props.keepProp.img}")`),

      getKeepById() {
        try {
          keepsService.getKeepById(props.keepProp)
          Modal.getOrCreateInstance('#detailsModal').show()
        } catch (error) {
          Pop.error(error.message, '[GETTING KEEP BY ID]')
        }
      },

      async removeKeep() {
        try {
          const isSure = await Pop.confirm(`Are you sure you want to delete ${props.keepProp.name}?`)

          if (!isSure) {
            return
          }
          await keepsService.removeKeep(props.keepProp.id)
          Pop.toast('Keep was deleted!')
        } catch (error) {
          Pop.error(error.message, '[DELETING KEEP]')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
  i {
    position: absolute;
    top: -0.3rem;
    right: -0.3rem;
    color: white;
    background-color: #FF5977;
    border-radius: 50%;
    height: 1rem;
    width: 1rem;
  }

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