import Vue from 'vue'
import Toasted from 'vue-toasted'
import { VueperSlides, VueperSlide } from 'vueperslides'

import globalExceptionHandling from '~/plugins/utils/globalExceptionHandling'
import configureToastedTemplates from '~/plugins/utils/configureToastedTemplates'
import nuxtLinkNavigator from '~/components/nuxtLinkNavigator/nuxtLinkNavigator'
// import SlickCarousel from '~/components/mainComponents/SlickCarousel'

Vue.use(Toasted, {
  position: 'bottom-right',
  duration: 5000,
  iconPack: 'fontawesome'
})
Vue.use(globalExceptionHandling)
Vue.use(configureToastedTemplates)
Vue.component(nuxtLinkNavigator.name, nuxtLinkNavigator)
Vue.component('vueper-slides', VueperSlides)
Vue.component('vueper-slide', VueperSlide)
