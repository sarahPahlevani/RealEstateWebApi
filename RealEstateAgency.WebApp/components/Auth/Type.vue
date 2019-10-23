<template>
  <div class="nav-bar-whole">
    <ul
      class="nav nav-tabs nav-justified mb-5 p-2 border nav-bar"
      id="myTab"
      role="tablist"
    >
      <li class="nav-item" :class="{ active: activeLink == 'Login' }">
        <LangLink
          :to="
            $route.query.redirect
              ? `/Login?redirect=${$route.query.redirect}`
              : '/Login'
          "
          ><span>Login</span></LangLink
        >
      </li>
      <li class="nav-item" :class="{ active: activeLink == 'Register' }">
        <LangLink
          :to="
            $route.query.redirect
              ? `/Register?redirect=${$route.query.redirect}`
              : '/Register'
          "
          ><span>Register</span></LangLink
        >
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  props: {
    activeLink: String
  }
}
</script>

<style scoped>
.nav-item {
  background: rgb(119, 119, 119);
  margin: 0 20px;
  color: #24abe3;
  border-radius: 4px;
}
.nav-item span {
  color: #fff;
  display: block;
  padding: 10px;
}
.nav-item span:hover {
  text-decoration: none;
}
.nav-item:hover {
}
.active {
  background-color: #24abe3 !important;
  color: #fff !important;
}
</style>
