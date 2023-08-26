<template>
  <div class="d-flex flex-column justify-content-end bg-img rounded elevation-4 p-2">
    <div class="d-flex justify-content-between align-items-center">
      <p class="card-name fs-4">{{ vaultProp.name }}</p>
      <i v-if="vaultProp.isPrivate" class="mdi mdi-lock-outline rounded-circle bg-light fs-5 px-1"></i>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { Vault } from '../models/Vault.js'

export default {
  props: {
    vaultProp: {
      type: Vault,
      required: true
    }
  },

  setup(props) {
    return {
      backgroundImage: computed(() => `url("${props.vaultProp.img}")`)
    }
  }
}
</script>

<style lang="scss" scoped>
  .bg-img {
    background-image: v-bind(backgroundImage);
    background-position: center;
    background-size: cover;
    height: 20vh;
  }
</style>