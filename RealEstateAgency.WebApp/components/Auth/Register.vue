<template>
  <form id="Register" class="card-body animated slideInRight" tabindex="500">
    <h3 class="pb-1">Register</h3>
    <div class="first-name">
      <input
        v-model="user.firstname"
        type="text"
        name="name"
        @keyup.enter="SubmitRegisterInfo"
      />
      <div
        v-if="errormsg.firstname && hasError"
        class="error vuelidate-invalid-feedback"
      >
        First Name is required.
      </div>
      <label>First Name</label>
    </div>
    <div class="last-name">
      <input
        v-model="user.lastname"
        type="text"
        name="name"
        @keyup.enter="SubmitRegisterInfo"
      />
      <div
        v-if="errormsg.lastname && hasError"
        class="error vuelidate-invalid-feedback"
      >
        Last Name is required.
      </div>
      <label>Last Name</label>
    </div>
    <!--    <div class="user-name">-->
    <!--      <input-->
    <!--        v-model="user.username"-->
    <!--        type="text"-->
    <!--        name="name"-->
    <!--        @keyup="SearchTimeOut"-->
    <!--        @keyup.enter="SubmitRegisterInfo"-->
    <!--      />-->
    <!--      <div-->
    <!--        v-if="errormsg.username && hasError"-->
    <!--        class="error vuelidate-invalid-feedback"-->
    <!--      >-->
    <!--        User Name is required.-->
    <!--      </div>-->
    <!--      &lt;!&ndash; Taken username &ndash;&gt;-->
    <!--      <div v-if="usernameTaken" class="error vuelidate-invalid-feedback">-->
    <!--        User Name has been taken.-->
    <!--      </div>-->
    <!--      <label>User Name</label>-->
    <!--    </div>-->
    <div class="mail">
      <input
        v-model="user.email"
        type="email"
        name="mail"
        @keyup="SearchTimeOut"
        @keyup.enter="SubmitRegisterInfo"
      />
      <div
        v-if="errormsg.email && hasError"
        class="error vuelidate-invalid-feedback"
      >
        Email is required.
      </div>
      <div v-if="emailTaken" class="error vuelidate-invalid-feedback">
        This email has been taken.
      </div>
      <label>Email</label>
    </div>
    <div class="passwd">
      <input
        v-model="user.password"
        type="password"
        name="password"
        @keyup.enter="SubmitRegisterInfo"
      />
      <div
        v-if="errormsg.password && hasError"
        class="error vuelidate-invalid-feedback"
      >
        Passswrod is required.
      </div>
      <div v-if="shortPassword" class="error vuelidate-invalid-feedback">
        Passswrod Must Be At Least 8 Characters.
      </div>
      <label>Password</label>
    </div>
    <div class="submit">
      <span
        class="btn btn-block"
        :class="{ 'btn-warning': loading, 'btn-primary': !loading }"
        @click="SubmitRegisterInfo"
      >
        <span v-if="loading" class="fa fa-circle-o-notch fa-spin mr-3"></span
        >{{ loading ? 'Signing up...' : 'Register' }}</span
      >
      <!-- <a class="btn btn-primary btn-block" href="/login">Register</a> -->
    </div>
    <p class="text-dark mb-0">
      Already have an account?
      <LangLink
        :to="
          $route.query.redirect
            ? `/Login?redirect=${$route.query.redirect}`
            : '/Login'
        "
        ><span class="text-primary ml-1">Log In</span></LangLink
      >
    </p>
  </form>
</template>

<script>
// import { async } from 'q'
const Cookie = process.client ? require('js-cookie') : undefined
export default {
  name: 'Register',
  data() {
    return {
      loading: false,
      user: {
        firstname: '',
        lastname: '',
        username: '',
        email: '',
        password: ''
      },
      errormsg: {
        firstname: false,
        lastname: false,
        email: false,
        password: false
      },
      hasError: false,
      shortPassword: false,
      emailTaken: false
    }
  },
  methods: {
    RequiredFields() {
      // if (this.user.username === '') {
      //   this.errormsg.username = true
      // }
      this.errormsg.firstname = !this.user.firstname
      this.errormsg.lastname = !this.user.lastname
      this.errormsg.email = !this.user.email
      this.errormsg.password = !this.user.password
      this.hasError =
        this.hasError ||
        this.errormsg.password ||
        this.errormsg.lastname ||
        this.errormsg.email ||
        this.errormsg.firstname
    },
    CheckPassword() {
      // TODO: if this can be changed it would be great
      this.shortPassword = this.user.password.length < 8
      this.hasError = this.hasError || this.shortPassword
    },
    SearchTimeOut() {
      if (this.timer) {
        clearTimeout(this.timer)
        this.timer = null
      }
      this.timer = setTimeout(() => {
        this.ValidUserName()
      }, 650)
    },
    async ValidUserName() {
      try {
        if (!this.user.email) return
        this.user.username = this.user.email
        const res = await this.$axios({
          method: 'post',
          url: '/auth/checkUser',
          data: {
            username: this.user.username,
            email: this.user.email
          }
        })

        this.emailTaken = res.data.emailHasProblem
        this.usernameTaken = res.data.usernameHasProblem
        this.hasError = this.hasError || this.emailTaken || this.usernameTaken
      } catch (err) {
        this.$handleError(err.response)
      }
    },
    SubmitRegisterInfo() {
      this.hasError = false
      this.CheckPassword()
      this.RequiredFields()
      this.ValidUserName()
      if (!this.hasError && !this.loading) this.Register()
    },
    async Register() {
      this.loading = true
      try {
        this.user.recaptchaToken = await this.$recaptcha.execute('login')
        const res = await this.$axios({
          method: 'post',
          url: '/auth/register',
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
        console.error(err)
        this.$handleError(err.response)
      } finally {
        this.loading = false
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
