<template>
  <div>
    <carousel
      class="custom-carousel-item"
      :margin="22"
      :dots="false"
      :responsive="{
        0: { items: 1, nav: false },
        768: { items: 2, nav: false },
        1024: { items: 4, nav: false }
      }"
    >
      <PropertyCard
        v-for="(propertyItem, index) in properties"
        :key="index"
        :property="propertyItem"
      />
      <template slot="prev" class="arrow-btns"
        ><button
          type="button"
          role="presentation"
          class="owl-button next-prev-buttons"
        >
          <span class="prev">‹</span>
        </button></template
      >

      <template slot="next" class="arrow-btns"
        ><button
          type="button"
          role="presentation"
          class="owl-button next-prev-buttons"
        >
          <span class="prev">‹</span>
        </button></template
      >
    </carousel>
  </div>
</template>

<script>
import PropertyCard from './PropertyCard'

let carousel
if (process.client) {
  carousel = require('vue-owl-carousel')
}
export default {
  props: ['properties', 'propertyName'],
  name: 'PropertyCarousel',
  components: {
    PropertyCard,
    carousel
  }
}
</script>
<style scoped>
.custom-carousel-item {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  position: relative;
  padding: 0 20px;
}
.arrow-btns {
  top: 50%;
}
.next-prev-buttons {
  display: none;
}
</style>
