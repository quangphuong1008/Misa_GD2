<template>
  <div class="m-text-field">
    <div class="m-text-field__label">
      <div class="m-text-tittle">{{ label }}</div>
      <div v-if="required" class="m-text-field-required">&nbsp;*</div>
    </div>
    <input type="date" :value="value" @input="updateValue($event.target.value)" :class="{'m-input-error': checkInput}" :title="(checkInput) ? errorText: null" :placeholder='[[ placeholder ]]'
    :style="{ height: height }" :disabled="disable"
    />
  </div>
</template>

<script>
export default {
  props: {
    label: String,
    required: Boolean,
    value: String,
    checkInput: Boolean,
    errorText: String,
    placeholder: String,
    height: String,
    disable: {
      default: false,
      type: Boolean
    }
  },
  model: {
    prop: "value"
  },
  methods:{
    updateValue(value){
      this.$emit('input', value);
    }
  }
};
</script>

<style scope>

.m-text-field {
  width: 100%;
}
.m-text-field input[type="text"] {
  outline: none;
  font-size: 13px;
  border-radius: 2px;
  border: 1px solid #babec5;
  height: var(--input-height);
  padding-left: 10px;
  padding-right: 12px;
  box-sizing: border-box;
  width: 100%;
}

.m-text-field input[type="text"]:hover:not(:focus) {
  outline: 1px solid #e2e2e2;
}
.m-text-field input[type="text"]:focus {
  border: 1px solid #2ca01c;
  
}
.m-text-field__label {
  display: flex;
}
.m-text-tittle {
  font-size: 12px;
  font-family: Notosans-Regular;
  color: #111;
  font-weight: 700;
}
.m-text-field-required {
  color: red;
  font-size: 13px;
  font-family: Notosans-Regular;
}
.m-input-error{
  border: 1px solid red !important;;
}

</style>