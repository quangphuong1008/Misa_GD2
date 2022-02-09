<template>
  <div class="m-table-address">
    <div class="m-table" style="padding: 0">
      <table>
        <tbody>
          <tr
            v-for="(field, index) in datas"
            :key="index"
            @click="updateValue(index)"
            @change="update()"
            :class="select == index ? 'selected' : ''"
          >
            <td style="width: 400px">
              <div v-if="select == index">
                <input type="text" v-model="field.address" />
              </div>
              <p v-else>{{ field.address }}</p>
            </td>
            <td style="min-width: 40px; width: 40px; padding: 0">
              <div class="btn-18 mi-delete" style="margin-left: 10px"></div>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="footer-table-address">
        <button @click="datas.push({}), (select += 1)">Thêm dòng</button>
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
      select: -1,
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
    value() {
      this.datas = this.value;
    },
  },
  methods: {
    updateValue(value) {
      this.select = value;
    },
    update() {
      //let val = JSON.stringify(this.datas);
      this.$emit("input", this.datas);
    },
  },

  filters: {
    // Filter ngày tháng
  },
};
</script>

<style scope>
.m-table-address {
  box-sizing: border-box;
}
.m-table-address table {
  display: inline-block;
  overflow-y: scroll;
  max-height: 137px;
}
.m-table-address .m-table {
  padding: 0;
}
.m-table-address th {
  width: 100%;
  box-sizing: border-box;
}
.m-table-address td {
  border-top: 1px solid #c7c7c7;
}
.m-table-address .selected {
  background-color: #f4f5f8 !important;
}
.m-table-address input {
  padding: 6px 10px;
  width: 335px;
  border: 1px solid #babec5 !important;
}
.m-table-address input[type="text"]:focus {
  border: 1px solid #2ca01c !important;
  outline: none;
}
.footer-table-address {
  padding-top: 5px;
  position: fixed;
}
.footer-table-address button {
  height: 20px;
  background-color: #fff;
  margin-right: 6px;
  border-radius: 4px;
  font-weight: 600;
  font-size: 12px;
}
</style>