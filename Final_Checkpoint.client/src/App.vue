<template>
  <header class="container-fluid order-2 order-md-1">
    <Navbar />
  </header>
  <main class="container-fluid order-1 order-md-2">
    <section class="row justify-content-center">
      <router-view />
    </section>
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
  @import "./assets/scss/main.scss";

  :root {
    --main-height: calc(100vh - 64px);
  }

  body {
    background: #FEF6F0;
    font-family: 'Oxygen', sans-serif;
  }

  header {
    position: sticky;
    top: 0;
  }

  p {
    margin: 0;
  }

  .masonry {
    columns: 4 150px;
    column-gap: 1rem;

    div {
      display: inline-block;
      width: 100%;
    }

    @for $i from 1 through 36 {
      .masonry-item:nth-child(#{$i}) {
        $h: (random(200) + 150)+px;
        height: $h;
      }
    }
  }

  .card-name {
    color: white;
    font-weight: bold;
    text-shadow: 0px 4px 5px black;
  }

  @media screen and (max-width: 768px) {
    header {
      bottom: 0;
    }
  }
</style>