export default {
  mode: 'universal',
  /*
   ** Headers of the page
   */
  head: {
    title: process.env.npm_package_name || '',
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      {
        hid: 'description',
        name: 'description',
        content: process.env.npm_package_description || ''
      }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' },
      {
        rel: 'stylesheet',
        href:
          'https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.7.2/animate.min.css'
      },
      {
        rel: 'stylesheet',
        href: 'https://unpkg.com/vueperslides/dist/vueperslides.css'
      },
      {
        rel: 'stylesheet',
        href:
          '//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css',
        media: 'all'
      }
    ]
  },
  router: {
    middleware: 'i18n'
  },
  /*
   ** Customize the progress-bar color
   */
  loading: { color: '#82ccdd' },
  loadingIndicator: { name: 'circle', color: '#4a69bd' },
  /*
   ** Global CSS
   */
  css: [
    '~/assets/plugins/bootstrap-4.3.1-dist/css/bootstrap.min.css',
    '~/assets/css/style.css',
    '~/assets/css/icons.css',
    '~/assets/plugins/horizontal-menu/horizontal.css',
    '~/assets/plugins/select2/select2.min.css',
    '~/assets/plugins/cookie/cookie.css',
    '~/assets/plugins/owl-carousel/owl.carousel.css',
    '~/assets/plugins/scroll-bar/jquery.mCustomScrollbar.css',
    '~/assets/skins/color-skins/color1.css'
    // 'vueperslides/dist/vueperslides.css'
  ],
  /*
   ** Plugins to load before mounting the App
   */
  plugins: [
    '~/plugins/i18n.js',
    '~/plugins/axios.js',
    '~/plugins/eventBus.js',
    { src: '~/plugins/filters.js', ssr: false },
    { src: '~/plugins/globalComponents.js', ssr: false },
    '~/plugins/auth.js'
  ],
  /*
   ** Nuxt.js dev-modules
   */
  buildModules: [
    // Doc: https://github.com/nuxt-community/eslint-module
    '@nuxtjs/eslint-module'
  ],
  /*
   ** Nuxt.js modules
   */
  modules: ['@nuxtjs/axios', '@nuxtjs/moment', '@nuxtjs/recaptcha'],
  /*
   ** Build configuration
   */
  env: {},
  axios: {
    baseURL:
      process.env.NODE_ENV === 'production'
        ? 'http://localhost:5001/api/'
        : 'http://localhost:5000/api/'
  },
  recaptcha: {
    hideBadge: true, // Hide badge element (v3)
    language: 'hu', // Recaptcha language (v2)
    siteKey: '6Lf4k7oUAAAAAM13diHiv7UUTmaiMQsGcHGcN7LY', // Site key for requests
    version: 3 // Version
  },
  build: {
    extend(config, { isServer }) {
      if (isServer) {
        config.externals = [
          require('webpack-node-externals')({
            whitelist: [/^vue-slick/]
          })
        ]
      }
    }
    /*
     ** You can extend webpack config here
     */
    // extend(config, ctx) {}
  }
}
