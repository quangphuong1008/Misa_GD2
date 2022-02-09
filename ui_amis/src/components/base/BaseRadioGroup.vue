<template>
  <div class="m-radio-group">
    <div class="m-radio-group__label">{{ label }}</div>
    <div class="m-radio-group__list">
      <div class="m-radio-item" v-for="item in items" :key="item.id">
        <input
          type="radio"
          :name="name"
          :value="item.id"
          :checked="item.id == value"
          @change="updateValue(item.id)"
        />
        <span>{{ item.name }}</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    label: String,
    value: String,
    items: Array,
    name: String,
  },
  methods: {
    updateValue(value) {
      this.$emit("input", value);
    },
  },
};
</script>

<style scope>
.m-radio-group .m-radio-group__list {
  display: flex;

  padding-top: 5px;
  padding-bottom: 6px;
}
.m-radio-group .m-radio-group__list .m-radio-item {
  display: flex;
  align-items: center;
  margin-right: 20px;
}
.m-radio-group .m-radio-group__list .m-radio-item span {
  margin-left: 10px;
}
.m-radio-group .m-radio-group__list input[type="radio"] {
  width: 18px;
  height: 18px;
  border-radius: 50%;

  -webkit-appearance: none;
  outline: none;
  border: 1px solid gray;
  box-sizing: border-box;
}

.m-radio-group .m-radio-group__list input[type="radio"]:checked::after {
  width: 10px;
  height: 10px;
  border-radius: 15px;
  top: 2px;
  left: 2px;
  position: relative;
  background-color: #2ca01c;
  content: "";
  display: inline-block;
  visibility: visible;
  border-radius: 50%;
  border: 1px solid #fff;
}
.m-radio-group .m-radio-group__list input[type="radio"]:checked {
  border-color: #2ca01c;
}

.m-radio-group .m-radio-group__label {
  font-size: 12px;
  font-family: Notosans-Regular;
  color: #111;
  font-weight: 700;
}
.m-radio-group {
  display: flex;
  flex-direction: column;
  width: 100%;
  padding-left: 10px;
}
</style>