<template>
  <div class="m-combobox">
    <div class="m-combobox__label">
      <div class="m-text-tittle">{{ label }}</div>
      <div v-if="required" class="m-combobox-required">&nbsp;*</div>
    </div>
    <div class="m-combobox__body">
      <input
        ref="input"
        class="m-combobox-input"
        v-model="textSearch"
        type="text"
        :class="{ 'm-input-error': checkInput || isError }"
        :title="checkInput ? errorText : null"
        :placeholder="[[placeholder]]"
        @input="clickout"
        @focus="focusInput"
        @blur="blurCbb"
        :disabled="disable"
      />
      <div class="btn-dropdown" style="right: 33px" v-if="icon">
        <div
          class="btn-dropdown-icon plus-success"
          :class="{ rotate: isShow }"
        ></div>
      </div>
      <div class="btn-dropdown" @click="dropdown()" v-if="!disable">
        <div
          class="btn-dropdown-icon arrow-cb"
          :class="{ rotate: isShow }"
        ></div>
      </div>
      <div v-if="isShow" class="m-combobox__menu" :style="[{ width: width },{ right: location }]">
        <div class="m-table">
          <table>
            <thead v-if="!noneThead">
              <tr style="height: 35px !important">
                <th
                  v-for="(field, index) in fields"
                  :key="index"
                  :style="[
                    { 'text-align': field.type === 'date' ? 'center' : '' },
                    { width: field.width },
                    {
                      height: '32px !important',
                    },
                  ]"
                >
                  {{ field.text }}
                </th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(data, index) in originalData"
                :key="index"
                @click="updateValueAndText(data[id], $event, data)"
                :class="[checkItem(data[id]) ? 'selected' : '']"
              >
                <td
                  v-for="(field, index) in fields"
                  :key="index"
                  :style="field.type === 'date' ? 'text-align:center' : ''"
                  style="height: 32px !important"
                >
                  {{ data[field.name] }}
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
      checkSelect: false,
      isError: false
    };
  },
  props: {
    departments: Array,
    label: String,
    required: Boolean,
    value: String,
    checkInput: Boolean,
    errorText: String,
    fields: Array,
    datas: Array,
    id: String,
    textShow: String,
    noneThead: Boolean,
    width: String,
    icon: Boolean,
    placeholder: String,
    disable: {
      default: false,
      type: Boolean,
    },
    location: String
  },
  mounted() {

  },
  beforeDestroy() {

  },
  created() {
    this.focusInput();
    //this.originalData = this.datas;
  },
  watch: {
    checkSelect() {
      if (!this.checkSelect) {
        this.textSearch = "";
      }
    },
    value(value) {
      if (value != "" || value != null) {
        let check = true;
        this.originalData = this.datas;
        for (let i = 0; i < this.datas.length; i++) {
          if (value == this.datas[i][this.id])
            this.$refs.input.value = this.datas[i][this.textShow];
          check = false;
        }
        this.textSearch = this.$refs.input.value;
        if (check) {
          this.textSearch = "";
        }
      } else {
        this.textSearch = "";
        this.$refs.input.value = "";
      }
    },
    textSearch: function (val) {
      let me = this;

      if (this.timemout) {
        clearTimeout(this.timemout);
      }
      this.timemout = setTimeout(() => {
        let check = false;
        me.datas.forEach((data) => {
          if (val == data[this.textShow]) {
            check = true;
          }
        });
        if (check == false) {
          me.updateValue(null);
        }
      }, 350);

      if (val != "") {
        me.originalData = me.datas.filter(function (item) {
          if (
            String(item[me.textShow])
              .toLowerCase()
              .includes(String(val.toLowerCase()))
          ) {
            return true;
          }
          return false;
        });
      } else {
        me.originalData = me.datas;
      }
    },
  },
  methods: {
    setErrorValidation(value) {
      const me = this;
      me.isError = value;
    },

    checkItem(value) {
      let temp = false;
      if (this.value == value) {
        temp = true;
      }
      return temp;
    },
    blurCbb() {
      this.isShow = false;
    },
    focusInput() {
      if (this.id && this.datas && this.value && this.textShow) {
        for (let i = 0; i < this.datas.length; i++) {
          if (this.value == this.datas[i][this.id])
            this.textSearch = this.datas[i][this.textShow];
        }
      } else {
        this.textSearch = "";
      }
    },
    dropdown() {
      this.isShow = !this.isShow;
      this.originalData = this.datas;
    },

    clickout() {
      this.isShow = true;
    },
    updateValueAndText(value, event, data) {
      console.log(event);
      this.isShow = false;
      this.updateValue(value);
      for (let i = 0; i < this.datas.length; i++) {
        if (value == this.datas[i][this.id])
          this.textSearch = this.datas[i][this.textShow];
      }

      console.log(value);
      this.$emit("selectValue", data);
    },
    updateValue(value) {
      this.setErrorValidation(false);
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
  border-left: 1px;
}
.btn-dropdown:hover {
  background-color: #ddd;
}
.m-combobox tbody tr:hover {
  background-color: #ebedf0;
  color: #35bf22;
}
.selected {
  background-color: #ebedf0;
  color: #35bf22;
}
.m-combobox .m-combobox__body .m-combobox__menu .menu-body {
  max-height: 160px;
  overflow-x: hidden;
  overflow-y: auto;
  padding: 2px 1px;
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
.btn-32 {
  width: 32px;
  height: 32px;
  padding: 0 6px;
  box-sizing: content-box;
  cursor: pointer;
}
.m-combobox td,
.m-combobox th {
  border: none !important;
}
.m-combobox table {
  border-collapse: separate !important;
  overflow-y: scroll;
}
.m-combobox .m-table {
  padding: 0;
  z-index: 9999999999999999999;
  background-color: rgb(253, 253, 253);
  max-height: 200px;
  overflow: auto;
}
.m-combobox tbody {
  overflow-y: scroll;
}
</style>