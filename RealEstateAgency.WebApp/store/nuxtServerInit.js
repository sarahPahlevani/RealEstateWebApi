const cookieParser = process.server ? require('cookieparser') : undefined

export async function nuxtServerInit(vuex, context) {
  const lang = context.route.params._lang || 'en'
  const req = context.req
  try {
    const [
      propertyTypes,
      propertyLabels,
      propertyStatuses,
      propertyFeatures,
      cities,
      regions,
      languages,
      info,
      realEstateInfo,
      agents
    ] = await Promise.all([
      this.$axios.$get('propertyType/getAllByLanguage/' + lang),
      this.$axios.$get('propertyLabel/getAllByLanguage/' + lang),
      this.$axios.$get('propertyStatus/getAllByLanguage/' + lang),
      this.$axios.$get('propertyFeature/getAllByLanguage/' + lang),
      this.$axios.$get('city/getAllByLanguage/' + lang),
      this.$axios.$get('region/getAllByLanguage/' + lang),
      this.$axios.$get('language'),
      this.$axios.$get('public/siteInfo'),
      this.$axios.$get('public/realEstateInfo'),
      this.$axios.$get('agent/getWebAppAgents')
    ])
    vuex.commit('SET_PROPERTY_TYPES', propertyTypes)
    vuex.commit('SET_PROPERTY_STATUSES', propertyStatuses)
    vuex.commit('SET_PROPERTY_LABELS', propertyLabels)
    vuex.commit('SET_PROPERTY_FEATURES', propertyFeatures)
    vuex.commit('SET_CITIES', cities)
    vuex.commit('SET_REGIONS', regions)
    vuex.commit('SET_LANGUAGES', languages)
    vuex.commit('SET_INFO', info)
    vuex.commit('SET_REAL_ESTATE_INFO', realEstateInfo)
    vuex.commit('SET_AGENTS', agents)

    if (req.headers.cookie) {
      const parsed = cookieParser.parse(req.headers.cookie)

      if (parsed.token) {
        try {
          const user = await this.$axios.$get('auth', {
            headers: {
              Authorization: `Bearer ${parsed.token}`
            }
          })
          vuex.commit('SET_USER', user.data)
          vuex.commit('SET_TOKEN', parsed.token)
        } catch (err) {
          console.error(err)
        }
      }
    }
  } catch (e) {
    console.log(e)
  }
}
