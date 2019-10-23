<template>
  <div class="header-main">
    <TopHeader class="bottom-header" />
    <BottomHeader class="bottom-header" />
  </div>
</template>
<script>
import TopHeader from './subHeaders/TopHeader'
import BottomHeader from './subHeaders/BottomHeader'
export default {
  name: 'Header',
  components: {
    TopHeader,
    BottomHeader
  }
}
</script>

<style>
.bottom-header {
  position: sticky !important;
  top: 0;
}
</style>
