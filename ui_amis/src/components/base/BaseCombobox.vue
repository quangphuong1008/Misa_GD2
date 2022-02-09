<template>
  <div class="m-combobox">
    <div class="m-combobox__label">
      <div class="m-text-tittle">{{ label }}</div>
      <div v-if="required" class="m-combobox-required">&nbsp;*</div>
    </div>
    <div class="m-combobox__body">
      <input
        ref="Department"
        class="m-combobox-input"
        v-model="textSearch"
        type="text"
        :title="checkInput ? errorText : null"
        @input="clickout"
      />
      <div class="btn-dropdown" @click="dropdown()">
        <div
          class="btn-dropdown-icon arrow-cb"
          :class="{ rotate: isShow }"
        ></div>
      </div>
      <div v-if="isShow" class="m-combobox__menu">
        <div class="menu-body">
          <table>
            <tbody>
              <tr
                v-for="(data, index) in datas"
                :key="index"
                :class="[data.value == value ? 'selected' : '']"
                @click="updateValueAndText(data.value, $event)"
              >
                <td style="width: 100%; text-align: center">
                  {{ data.value }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isShow: false,
      textSearch: "",
      originalData: [],
      timeout: null,
    };
  },
  props: {
    datas: Array,
    label: String,
    required: Boolean,
    value: String,
    checkInput: Boolean,
    errorText: String,
   
  },
  mounted() {
    this.originalData = this.datas;
  },

  watch: {
    textSearch: function () {
      // let me = this;
      // let check = false;

      // if (check == false) {
      //   // if(val == ""){
      //   me.updateValue("");
      // }

      // if (val) {
      //   me.originalData = me.departments.filter(function (item) {
      //     if (item.departmentName.toLowerCase().includes(val.toLowerCase())) {
      //       return true;
      //     }
      //     return false;
      //   });
      // } else {
      //   me.originalData = me.departments;
      // }
    },
  },
  methods: {
    dropdown() {
      this.isShow = !this.isShow;
      this.originalData = this.departments;
    },

    clickout() {
    },
    updateValueAndText(value, event) {
      this.isShow = false;
      this.$emit("input", value);
      this.textSearch = event.target.innerText;
    },
    updateValue(value) {
      this.$emit("input", value);
    },
  },
};
</script>

<style scope>
:root {
  --input-height: 32px;
}
.rotate {
  transform: rotateX(3.142rad);
}
.btn-dropdown .btn-dropdown-icon {
  height: 16px;
  width: 16px;
}
.btn-dropdown {
  position: absolute;
  top: 1px;
  right: 1px;
  width: 32px;
  height: 30px;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: transparent;
  cursor: pointer;
}
.btn-dropdown:hover {
  background-color: #ddd;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-body tbody tr {
  height: var(--input-height);
  width: 100%;
  cursor: pointer;
  background-color: #fff;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-body tbody tr:hover {
  background-color: #ebedf0;
  color: #35bf22;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-body tbody tr.selected {
  background-color: #ebedf0;
  color: #35bf22;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-body table {
  width: 100%;
  border-collapse: collapse;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-body {
  max-height: 160px;
  overflow-x: hidden;
  overflow-y: auto;
  padding: 2px 1px;
}
.m-combobox .m-combobox__body .m-combobox__menu {
  border: 1px solid #babec5;
  border-radius: 2px;
  position: absolute;
  top: calc(100% + 2px);

  width: 100%;
  z-index: 9999;
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  -o-user-select: none;
  user-select: none;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-header {
  display: flex;
  background: #f4f5f8;
  align-items: center;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-header .menu-header__th {
  padding: 0 10px;
  height: var(--input-height);
  color: #111;
  font-family: Notosans-Regular;
  font-weight: 700;
  display: flex;
  align-items: center;
}
.m-combobox .m-combobox__body {
  position: relative;
  width: 100%;
}
.m-combobox {
  width: 100%;
  display: flex;
  flex-direction: column;
}

.m-combobox input[type="text"] {
  outline: none;
  font-size: 13px;
  border-radius: 2px;
  border: 1px solid #babec5;
  height: var(--input-height);
  padding-left: 10px;
  padding-right: 32px;
  box-sizing: border-box;
  width: 100%;
}

.m-combobox input[type="text"]:hover:not(:focus) {
  outline: 1px solid #e2e2e2;
}
.m-combobox input[type="text"]:focus {
  border: 1px solid #2ca01c;
}
.m-combobox__label {
  display: flex;
  width: 100%;
}
.m-text-tittle {
  font-size: 12px;
  font-family: Notosans-Regular;
  color: #111;
  font-weight: 700;
}
.m-combobox-required {
  color: red;
  font-size: 13px;
  font-family: Notosans-Regular;
}
.m-input-error {
  border: 1px solid red !important;
}
</style>