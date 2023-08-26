<template>
  <nav class="row navbar navbar-expand-lg navbar-dark py-0 px-3 elevation-3">
    <div class="col-4 d-flex order-2 order-md-1">
      <router-link :to="{ name: 'Home' }" class="d-none d-md-block btn selectable home">
        Home
      </router-link>
      <div class="dropdown">
        <button type="button" role="button" class="btn dropdown-toggle mx-2" data-bs-toggle="dropdown" aria-expanded="false">Create</button>

        <div class="dropdown-menu dropdown-menu-start p-0 create" aria-labelledby="createDropdown">
          <div class="list-group create">
            <div class="dropdown-item list-group-item-action fw-bold selectable p-1">
              new keep
            </div>
            <hr class="m-0 px-2">
            <div class="dropdown-item list-group-item-action fw-bold selectable p-1" @click="logout">
              new vault
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="col-4 d-flex order-1 order-md-2">
      <router-link class="rounded mx-md-auto ps-1 pe-4 title" :to="{ name: 'Home' }">
        <p>the<br>keepr<br>co.</p>
      </router-link>
    </div>

    <!-- LOGIN COMPONENT HERE -->
    <Login class="col-4 order-3" />
  </nav>
</template>

<script>
import Login from './Login.vue'

export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
  nav {
    height: 70px;
    background: #FEF6F0;
  }

  .title {
    color: black;
    border: 1.5px solid black;
    line-height: 20px;
  }

  .home {
    border-radius: 15px;
    background: #E9D8D6;
  }

  .create {
    border-radius: 10px;
    border: 1px solid #2D2D2D;
    background: #DED6E9;
  }

  .dropdown-item {
    border-radius: 10px;
  }
</style>