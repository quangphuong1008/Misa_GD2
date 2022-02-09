<template>
  <div class="m-table-bank">
    <div class="m-table" style="padding: 11px 8px">
      <table>
        <thead>
          <tr style="height: 35px !important">
            <th>Số tài khoản</th>
            <th>Tên ngân hàng</th>
            <th>Chi nhánh</th>
            <th style="width: 229px">tỉnh/tp của ngân hàng</th>
            <th style="min-width: 40px; width: 40px; padding: 0"></th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(field, index) in datas"
            :key="index"
            @click="updateValue(index, $event)"
            @change="update(datas)"
            :class="select == index ? 'selected' : ''"
          >
            <td style="height= 44px">
              <div v-show="select == index">
                <input type="text" v-model="field.bank_account" />
              </div>
              <p v-show="select != index">{{ field.bank_account }}</p>
            </td>

            <td>
              <div v-show="select == index">
                <input type="text" v-model="field.bank_branch_name" />
              </div>
              <p v-show="select != index">{{ field.bank_branch_name }}</p>
            </td>
            <td>
              <div v-show="select == index">
                <input type="text" v-model="field.bank_name" />
              </div>
              <p v-show="select != index" style="">{{ field.bank_name }}</p>
            </td>
            <td>
              <div v-show="select == index">
                <input type="text" v-model="field.bank_province_or_city" />
              </div>
              <p v-show="select != index">{{ field.bank_province_or_city }}</p>
            </td>
            <td>
              <div class="btn-18 mi-delete"></div>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="footer-table-bank">
        <button @click="datas.push({}), (select += 1), show()">
          Thêm dòng
        </button>
        <button @click="(datas = [{}]), (select = 0)">Xóa hết dòng</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {
      datas: [],
      select: null,
      bank: {
        bank_account: "",
        bank_branch_name: "",
        bank_name: "",
        bank_province_or_city: "",
      },
    };
  },
  props: {
    fields: Array,
    value: Array,
    btnText: String,
  },
  mounted() {
    if (this.value != [{}]) {
      this.datas = [{}];
    } else {
      this.datas = this.value;
    }
  },

  watch: {
   value(){
      this.datas = this.value;
   }
  },
  methods: {
    updateValue(value, event) {
      this.select = value;
      let temp = event.target.parentElement.getElementsByTagName("input")[0];

      setTimeout(function () {
        temp.focus();
      });
    },
    update(value) {
      this.$emit("input", value);
    },
    show() {
      console.log(this.datas);
    },
  },

  filters: {
    // Filter ngày tháng
  },
};
</script>

<style scope>
.m-table-bank {
  box-sizing: border-box;
}
.m-table-bank table {
  display: inline-block;
  overflow-y: scroll;
  max-height: 160px;
}
.m-table-bank th {
  width: 180px;
  box-sizing: border-box;
}
.m-table-bank .selected {
  background-color: #f4f5f8 !important;
}
.m-table-bank input {
  padding: 6px 10px;
  width: 100%;
  border: 1px solid #babec5 !important;
}
.m-table-bank input[type="text"]:focus {
  border: 1px solid #2ca01c !important;
  outline: none;
}
.footer-table-bank {
  padding-top: 5px;
}
.footer-table-bank button {
  height: 20px;
  background-color: #fff;
  margin-right: 6px;
  border-radius: 4px;
  font-weight: 600;
  font-size: 12px;
}
.btn-18 {
  height: 18px;
  width: 18px;
}
</style>