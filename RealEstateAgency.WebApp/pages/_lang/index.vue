<template>
  <div>
    <!--    filter section-->
    <FilterSection />
    <!-- end   filter section-->
    <!--    sharing section-->
    <SharingSection />
    <!--   end sharing section-->

    <!--Section-->
    <section class="sptb bg-patterns">
      <div class="container">
        <div class="section-title center-block text-center">
          <h2>HotOffer Properties</h2>
          <!--          <p>-->
          <!--            Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua-->
          <!--          </p>-->
        </div>
        <PropertyCarousel
          class="animated slideInLeft delay-2s fast"
          :properties="hotOfferProperties"
        />
      </div>
    </section>
    <!--/Section-->

    <!--Section-->
    <section class="sptb bg-white">
      <div class="container">
        <div class="section-title center-block text-center">
          <h2>Latest Properties</h2>
          <!--          <p>-->
          <!--            Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua-->
          <!--          </p>-->
        </div>
        <PropertyCarousel
          class="animated slideInLeft delay-1s slow"
          :properties="latestProperties"
        />
      </div>
    </section>
    <!--/Section-->

    <section class="sptb">
      <div class="container">
        <div class="section-title center-block text-center">
          <h2>Agents</h2>
        </div>
        <div class="row">
          <div
            v-for="agent in agentList"
            :key="agent.agentId"
            class="col-xl-4 col-lg-4 col-md-12"
          >
            <SingleAgent :agent="{ agent }" />
          </div>
        </div>
      </div>
    </section>
    <!--Section-->
    <section class="sptb bg-white">
      <Download />
    </section>
    <!--/Section-->
    <!-- Back to top -->
    <a href="#top" id="back-to-top"><i class="fa fa-rocket"></i></a>
  </div>
</template>
<script>
import FilterSection from '~/components/homepage/FilterSection'
import SharingSection from '~/components/homepage/SharingSection'
import PropertyCarousel from '~/components/mainComponents/PropertyCarousel'
import Download from '~/components/homepage/Download'
import SingleAgent from '~/components/homepage/singleAgent'
// import VueperSlider from '~/components/mainComponents/VueperSlider'
export default {
  components: {
    Download,
    PropertyCarousel,
    FilterSection,
    SharingSection,
    SingleAgent
    // VueperSlider
  },
  data() {
    return {
      agentList: []
    }
  },

  async asyncData({ $axios, store }) {
    const label = store.state.propertyLabels.filter((a) =>
      a.name.toLowerCase().includes('hot')
    )[0]
    const [getLatestData, getHotOfferData] = await Promise.all([
      $axios.$post('property/getWebAppPage', {
        filter: {
          isAscending: false,
          searchText: ''
        },
        pageSize: 10,
        pageNumber: 0
      }),
      $axios.$post('property/getWebAppPage', {
        filter: {
          isAscending: false,
          searchText: label ? 'property_label:' + label.id : ''
        },
        pageSize: 10,
        pageNumber: 0
      })
    ])
    return {
      latestProperties: getLatestData.items,
      hotOfferProperties: getHotOfferData.items
    }
  },
  created() {
    this.agentList = this.$store.getters.agents
  }
}
</script>
<style>
.slick-carousel {
  box-sizing: border-box;
  /* width: 100%; */
}
</style>
