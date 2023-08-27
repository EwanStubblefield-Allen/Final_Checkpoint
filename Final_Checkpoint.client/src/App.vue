<template>
  <header class="container-fluid order-2 order-md-1 z-1">
    <Navbar />
  </header>
  <main class="container-fluid order-1 order-md-2">
    <section class="row justify-content-center">
      <router-view />
    </section>
  </main>

  <DetailsModal />

  <KeepForm />

  <VaultForm />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import DetailsModal from './components/DetailsModal.vue'
import KeepForm from './components/KeepForm.vue'
import VaultForm from './components/VaultForm.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepForm, VaultForm, DetailsModal }
}
</script>
<style lang="scss">
  @import "./assets/scss/main.scss";

  :root {
    --main-height: calc(100vh - 64px);
  }

  body {
    background: #FEF6F0;
    font-family: 'Marko One', serif;
  }

  header {
    position: sticky;
    top: 0;
  }

  p {
    margin: 0;
  }

  .underline {
    background-color: transparent;
    border: none !important;
    border-bottom: 1px solid gray !important;
    border-radius: 10px 10px 0 0;
  }

  .masonry {
    columns: 4 150px;
    column-gap: 1rem;

    div {
      display: inline-block;
      width: 100%;
    }

    @for $i from 1 through 300 {
      .masonry-item:nth-child(#{$i}) {
        height: (random(225) + 150)+px;
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