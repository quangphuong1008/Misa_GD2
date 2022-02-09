<template>
  <div class="m-combobox-panigation">
    <input type="text" readonly="readonly" v-model="itemSelected"/>
    <div class="btn-dropdown" @click="isShow = !isShow">
      <div class="btn-dropdown-icon arrow-cb" :class="{'rotate': isShow}"></div>
    </div>
    <div class="m-combobox-list" v-if="isShow">
      <div
        v-for="(item, index) in ListItem"
        :key="index"
        class="m-combobox-list__item" :class="{'selected': (item == itemSelected)}"
        @click="handleClickItem(item, index)"
      >
        {{ item }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isShow: false,
      itemSelected: String
    };
  },
  props: {
    ListItem: Array,
  },
  created(){
      this.itemSelected = this.ListItem[0];
  },
  methods:{
    handleClickItem(item, index){
      this.itemSelected = item; 
      this.isShow = false;
      let pageRecord = 10;
      if(index == 0) pageRecord = 10;
      else if(index == 1) pageRecord = 20;
      else if(index == 2) pageRecord = 30;
      else if(index == 3) pageRecord = 50;
      else if(index == 4) pageRecord = 100;
      this.$emit('handleSelect', pageRecord);
    }
  }
};
</script>

<style scoped>
.selected{
    color: #fff;
    background-color: #2ca01c;
    pointer-events: none;
}
.m-combobox-list__item:hover {
  background-color: #ebedf0;
  color: #35bf22;
}
.m-combobox-list__item {
  padding: 0 14px 0 10px;
  height: 32px;
  align-items: center;
  display: flex;
  cursor: pointer;
}
.m-combobox-list {
  border: 1px solid #babec5;
  border-radius: 2px;
  box-shadow: 0 -8px 34px 0 rgb(0 0 0 / 5%);
  position: absolute;
  bottom: calc(100% + 2px);
  background-color: #fff;
  width: 100%;
   z-index: 999;
}
.btn-dropdown .btn-dropdown-icon {
  height: 16px;
  width: 16px;
}
.rotate{
  transform: rotateX(3.142rad);
}
.btn-dropdown {
  position: absolute;
  top: 1px;
  right: 1px;
  width: 32px;
  height: 29px;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: transparent;
  cursor: pointer;
}
.btn-dropdown:hover {
  background-color: #ddd;
}
.m-combobox-panigation {
  position: relative;
  width: 200px;
  margin-left: 16px;
  margin-right: 16px;
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  -o-user-select: none;
  user-select: none;
}
.m-combobox-panigation input {
  padding: 6px 32px 6px 10px;
  height: 30px;
  outline: none;
  border: 1px solid #babec5;
  box-sizing: border-box;
  width: 100%;
}
.m-combobox input[type="text"]:hover:not(:focus) {
  outline: 1px solid #e2e2e2;
}
.m-combobox-panigation input[type="text"]:focus {
  border: 1px solid #2ca01c;
}
</style>>
