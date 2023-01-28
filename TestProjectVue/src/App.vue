

<template>
  <div class="modal-mask" v-if="showError">
    <div class="modal-wrapper">
      <div class="modal-container">
        <ErrorMessage />
      </div>
    </div>
  </div>
  <div class="modal-mask" v-if="this.$store.getters.loading">
    <PageLoader />
  </div>
  <router-view></router-view>
</template>
<script>

import ErrorMessage from "@/Components/Error.vue";
import PageLoader from "@/Components/Loader.vue";
import "./style/style.css"

export default {
  name: 'app',
  data() {
    return {

    };
  },
  components: {
    ErrorMessage,
    PageLoader
  },
  computed: {
    showError() {
      return this.$store.getters.error;
    }
  },
  created() {

  },
  methods: {
    onIndexClick() {
      this.$router.push("/CommetReport");
    }
  }
};
</script>

<style>

</style>
