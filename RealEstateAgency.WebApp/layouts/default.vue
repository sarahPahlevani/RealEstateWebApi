<template>
  <div class="layout">
    <TopHeader />
    <BottomHeader class="headerSticky" />
    <nuxt class="pages" />
    <Footer />
  </div>
</template>
<script>
// import Header from '~/components/header/Header'
import TopHeader from '~/components/header/subHeaders/TopHeader'
import BottomHeader from '~/components/header/subHeaders/BottomHeader'
import Footer from '~/components/Footer'
export default {
  components: {
    Footer,
    BottomHeader,
    TopHeader
    // Header,
  },
  beforeDestroy() {
    this.$bus.$off('internal-server', this.handleInternalServerError)
    this.$bus.$off('bad-request', this.handleBadRequestError)
    this.$bus.$off('access-denied', this.handleAccessDeniedError)
  },
  created() {
    this.$bus.$on('internal-server', this.handleInternalServerError)
    this.$bus.$on('bad-request', this.handleBadRequestError)
    this.$bus.$on('access-denied', this.handleAccessDeniedError)
  },
  methods: {
    handleInternalServerError(msg) {
      this.$toasted.global.error(msg)
    },
    handleBadRequestError(msg) {
      this.$toasted.global.error(msg)
    },
    handleAccessDeniedError(msg) {
      this.$toasted.global.error(msg)
    }
  }
}
</script>
<style>
.headerSticky {
  position: sticky;
  top: 0;
  z-index: 1000;
}
</style>
