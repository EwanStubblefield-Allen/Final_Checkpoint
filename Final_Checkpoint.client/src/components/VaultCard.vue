<template>
  <router-link :to="{ name: 'Vault', params: { vaultId: vaultProp.id } }" class="d-flex flex-column justify-content-end bg-img rounded elevation-4 p-2">
    <div class="d-flex justify-content-between align-items-end">
      <p :class="{ 'w-75': vaultProp.isPrivate }" class="card-name fs-4">{{ vaultProp.name }}</p>
      <i v-if="vaultProp.isPrivate" class="mdi mdi-lock-outline d-flex justify-content-center align-items-center bg-light fs-5"></i>
    </div>
  </router-link>
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
    height: 25vh;
  }

  .bg-light {
    height: 2rem;
    width: 2rem;
    border-radius: 50%;
  }
</style>