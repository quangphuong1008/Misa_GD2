<template>
  <div class="m-table-capayment">
    <div class="m-table" style="padding: 11px 0">
      <table>
        <thead>
          <tr style="height: 35px !important">
            <th>TK có</th>
            <th>Diễn giải</th>
            <th>Tk nợ</th>
            <th>số tiền</th>
            <th>đối tượng</th>
            <th>tên đối tượng</th>
            <th>tk ngân hàng</th>
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
            <td>
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
              <div v-show="select == index">
                <input type="text" v-model="field.bank_province_or_city" />
              </div>
              <p v-show="select != index">{{ field.bank_province_or_city }}</p>
            </td>
            <td>
              <div v-show="select == index">
                <input type="text" v-model="field.bank_province_or_city" />
              </div>
              <p v-show="select != index">{{ field.bank_province_or_city }}</p>
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
    };
  },
  mounted() {
    if (this.value != null) {
      this.datas = JSON.parse(this.value);
      console.log(1);
    } else {
      this.datas.push({});
    }
    // if(this.select != null){
    // this.$parent.$refs.accountObjectBankAccount.$el.querySelector("input").focus();
    // }
  },
  props: {
    fields: Array,
    value: String,
    btnText: String,
  },
  watch: {},
  methods: {
     
    updateValue(value, event) {
      this.select = value;
      let temp = event.target.parentElement.getElementsByTagName("input")[0];

      setTimeout(function () {
        temp.focus();
      });
    },
    update(value) {
      let val = JSON.stringify(this.datas);
      console.log(val);
      this.$emit("input", val);

      console.log(value);
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
.m-table-capayment {
  box-sizing: border-box;
}
.m-table-capayment table {
  display: inline-block;
  overflow-y: scroll;
  width: 100%;
}
.m-table-capayment th + tr {
  box-sizing: border-box;
}

.m-table-capayment .selected {
  background-color: #f4f5f8 !important;
}
.m-table-capayment input {
  padding: 6px 10px;
  width: 100%;
  border: 1px solid #babec5 !important;
}
.m-table-capayment p {
  padding: 6px 10px;
  width: 204.2px;
}

.m-table-capayment input[type="text"]:focus {
  border: 1px solid #2ca01c !important;
  outline: none;
}
.footer-table-capayment {
  padding-top: 5px;
  position: fixed;
}
.footer-table-capayment button {
  height: 20px;
  background-color: #fff;
  margin-right: 6px;
  border-radius: 4px;
  font-weight: 600;
  font-size: 12px;
}
</style>