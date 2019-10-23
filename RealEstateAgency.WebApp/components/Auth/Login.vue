<template>
  <form id="login" class="card-body animated slideInLeft" tabindex="500">
    <h3 class="pb-2">Login</h3>
    <div class="mail">
      <input
        v-model="user.usernameOrEmail"
        type="email"
        @focus="clear('usernameOrEmail')"
        name="mail"
        @keyup.enter="submitLoginInfo"
      />
      <div
        v-if="errormsg.usernameOrEmail && hasError"
        class="error vuelidate-invalid-feedback"
      >
        User Name or Email is required.
      </div>
      <label>Mail or User Name</label>
    </div>
    <div class="passwd">
      <input
        v-model="user.password"
        @focus="clear('pass')"
        type="password"
        name="password"
        @keyup.enter="submitLoginInfo"
      />
      <div
        v-if="errormsg.password && hasError"
        class="error vuelidate-invalid-feedback"
      >
        Password is required.
      </div>
      <label>Password</label>
    </div>
    <div class="submit">
      <span
        class="btn btn-block"
        :class="{ 'btn-warning': loading, 'btn-primary': !loading }"
        @click="submitLoginInfo"
      >
        <span v-if="loading" class="fa fa-circle-o-notch fa-spin mr-3"></span
        >{{ loading ? 'Logging in...' : 'Login' }}</span
      >
    </div>
    <p class="mb-2">
      <a>Forgot Password</a>
    </p>
    <p class="text-dark mb-0">
      Don't have account?<LangLink
        :to="
          $route.query.redirect
            ? `/Register?redirect=${$route.query.redirect}`
            : '/Register'
        "
        ><span class="text-primary ml-1">Register</span></LangLink
      >
    </p>
  </form>
</template>

<script>
const Cookie = process.client ? require('js-cookie') : undefined

export default {
  name: 'Login',
  data() {
    return {
      loading: false,
      user: {
        usernameOrEmail: '',
        password: ''
      },
      errormsg: {
        usernameOrEmail: false,
        password: false
      },
      hasError: false
    }
  },
  methods: {
    checkValidations() {
      // debugger
      if (this.user.usernameOrEmail === '') {
        this.errormsg.usernameOrEmail = true
        this.hasError = true
      }
      if (this.user.password === '') {
        this.errormsg.password = true
        this.hasError = true
      }
    },
    submitLoginInfo() {
      this.checkValidations()
      if (!this.hasError && !this.loading) this.login()
    },
    async login() {
      this.loading = true
      try {
        const res = await this.$axios({
          method: 'post',
          url: '/auth/login',
          data: this.user
        })

        const token = res.data.token
        this.$store.commit('SET_TOKEN', token)
        Cookie.set('token', token)
        await this.$store.dispatch('getUser', token)
        let redirect = '/profile'
        if (this.$route.params.lang)
          redirect = `/${this.$route.params.lang}/profile`
        if (this.$route.query && this.$route.query.redirect)
          redirect = this.$route.query.redirect

        this.$router.push(redirect)
      } catch (err) {
        this.$toasted.global.error(err.response.data.Message)
      } finally {
        this.loading = false
      }
    },
    clear(item) {
      switch (item) {
        case 'usernameOrEmail':
          this.errormsg.usernameOrEmail = false
          this.hasError = false
          break
        case 'pass':
          this.errormsg.password = false
          this.hasError = false
      }
    }
  }
}
</script>

<style scoped>
.vuelidate-invalid-feedback {
  width: 100%;
  margin-top: 0.25rem;
  font-size: 80%;
  color: #f86c6b;
}
</style>
