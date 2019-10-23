import Vuex from 'vuex'
import { nuxtServerInit } from './nuxtServerInit'
const createStore = () => {
  return new Vuex.Store({
    state: () => {
      return {
        locales: ['en', 'hu'],
        locale: 'en',
        token: null,
        user: null,
        propertyTypes: [],
        propertyStatuses: [],
        propertyLabels: [],
        propertyFeatures: [],
        cities: [],
        regions: [],
        languages: [],
        info: {},
        realEstateInfo: {},
        propertyDetail: {},
        userProfile: {},
        agents: []
      }
    },
    mutations: {
      SET_LANG(state, locale) {
        if (state.locales.includes(locale)) {
          state.locale = locale
        }
      },
      SET_USER(state, data) {
        state.user = data
      },
      SET_TOKEN(state, data) {
        state.token = data
      },
      SET_PROPERTY_TYPES(state, data) {
        state.propertyTypes = data
      },
      SET_PROPERTY_STATUSES(state, data) {
        state.propertyStatuses = data
      },
      SET_PROPERTY_LABELS(state, data) {
        state.propertyLabels = data
      },
      SET_PROPERTY_FEATURES(state, data) {
        state.propertyFeatures = data
      },
      SET_CITIES(state, data) {
        state.cities = data
      },
      SET_LANGUAGES(state, data) {
        data = data.map((i) => {
          const newLang = { ...i }
          newLang.name = newLang.code.toLowerCase().includes('en')
            ? 'EN - English'
            : 'HU - Magyar'
          return newLang
        })
        state.languages = data
      },
      SET_REGIONS(state, data) {
        state.regions = data
      },
      SET_INFO(state, data) {
        state.info = data
      },
      SET_REAL_ESTATE_INFO(state, data) {
        state.realEstateInfo = data
      },
      SET_PROPERTY_DETAIL(state, data) {
        state.propertyDetail = data
      },
      SET_USER_PROFILE(state, data) {
        state.userProfile = data
      },
      SET_AGENTS(state, data) {
        state.agents = data
      }
    },
    actions: {
      nuxtServerInit,
      async getUser({ commit }, token) {
        try {
          const user = await this.$axios.get('auth', {
            headers: {
              Authorization: `Bearer ${token}`
            }
          })
          commit('SET_USER', user.data)
        } catch (err) {
          console.error(err)
        }
      }
    },
    getters: {
      getToken: (state) => state.token,
      user: (state) => state.user,
      propertyTypes: (state) => state.propertyTypes,
      propertyStatuses: (state) => state.propertyStatuses,
      propertyLabels: (state) => state.propertyLabels,
      propertyFeatures: (state) => state.propertyFeatures,
      cities: (state) => state.cities,
      regions: (state) => state.regions,
      languages: (state) => state.languages,
      info: (state) => state.info,
      realEstateInfo: (state) => state.realEstateInfo,
      locale: (state) => state.locale,
      selectedLanguage: (state) => {
        return state.languages.filter((i) => i.code.includes(state.locale))[0]
      },
      propertyDetail: (state) => state.propertyDetail,
      userProfile: (state) => state.userProfile,
      agents: (state) => state.agents
    }
  })
}
export default createStore
