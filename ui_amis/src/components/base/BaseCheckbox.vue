<template>
  <div class="m-checkbox">
    <input type="checkbox" class="m-checked" :checked="checked" />
    <div class="m-checkbox-label" :style="checkLable ? '' : ' padding-left:10'">
      {{ label }}
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      checkLable: false,
    };
  },
  props: {
    label: {
      default: null,
      type: String,
    },
    checked: {
      default: false,
      type: Boolean,
    },
  },
  created() {
    if (this.value) {
      this.checkLable = true;
    }
  },
  watch: {
    label(value) {
      if (value) {
        this.checkLable = true;
      }
    },
  },
};
</script>

<style scope>
.m-checkbox-label {
  padding-left: 12px;
  font-size: 13px;
  font-family: Notosans-Regular;
  font-weight: normal;
  color: #111;
}
.m-checkbox {
  display: flex;

  margin: 0 auto;
}
.m-checkbox input[type="checkbox"] {
  width: 18px;
  height: 18px;
}
</style>

