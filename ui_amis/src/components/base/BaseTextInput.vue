<template>
  <div class="m-text-input">
    <input
      type="text"
      :placeholder="hintText"
      :value="value"
      @input="updateValue($event.target.value)"
    />
    <div :class="['icon', iconClass]" />
  </div>
</template>

<script>
export default {
  props: {
    hintText: String,
    iconClass: String,
    value: String,
  },
  methods: {
    updateValue(value) {
      this.$emit("input", value);
    },
  },
};
</script>

<style scope>

.m-text-input input {
  outline: none;
  font-size: 13px;
  border-radius: 2px;
  border: 1px solid #babec5;
  height: var(--input-height);
  padding-left: 10px;
  padding-right: 30px;
  box-sizing: border-box;
  width: 240px;
}
.m-text-input input:hover:not(:focus) {
  outline: 1px solid #e2e2e2;
}
.m-text-input input:focus {
  border: 1px solid #2ca01c;
}
.m-text-input {
  margin-right: 10px;
  position: relative;
}
.m-text-input input::placeholder {
  font-style: italic;
  font-size: 13px;
}
.m-text-input .icon {
  position: absolute;
  height: 16px;
  width: 16px;
  top: calc(50% - 8px);
  right: 12px;
  color: rgba(0, 0, 0, 0.4);
}
</style>