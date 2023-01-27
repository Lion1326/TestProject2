

<template>
  <div class="modal-mask" v-if="this.$store.state.error">
    <div class="modal-wrapper">
      <div class="modal-container">
        <ErrorMessage />
      </div>
    </div>
  </div>
  <router-view></router-view>
</template>
<script>

import ErrorMessage from "@/Components/Error.vue";
import "./style/style.css"

export default {
  name: 'app',
  data() {
    return {

    };
  },
  components: {
    ErrorMessage
  },
  computed: {

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
