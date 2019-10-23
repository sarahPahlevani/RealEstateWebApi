<template>
  <nuxt-link :to="generatedLink">
    <slot></slot>
  </nuxt-link>
</template>

<script>
export default {
  name: 'LangLink',
  data() {
    return {
      generatedLink: ''
    }
  },
  props: {
    to: {
      required: true,
      type: String
    }
  },
  created() {
    if (this.$route.params.lang)
      this.generatedLink = `/${this.$route.params.lang}${this.to}`
    else this.generatedLink = this.to
  }
}
</script>

<style></style>
