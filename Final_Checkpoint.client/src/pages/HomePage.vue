<template>
  <div class="col-12 col-md-10">
    <div class="masonry my-2">
      <div v-for="k in keeps" :key="k.id" class="masonry-item my-2">
        <KeepCard :keepProp="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, onUnmounted } from 'vue'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import KeepCard from '../components/KeepCard.vue'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    onMounted(() => {
      getKeeps()
    })
    onUnmounted(() => {
      AppState.keeps = []
    })

    async function getKeeps() {
      try {
        await keepsService.getKeeps()
      }
      catch (error) {
        Pop.error(error.message, '[GETTING KEEPS]')
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss"></style>