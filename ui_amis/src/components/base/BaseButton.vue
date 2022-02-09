<template>
  <button :style="[{background : backgroundcolor}, {color: '#ffffff'}]" @click="handleClick">
      <slot/>
  </button>
</template>
 
<script>
export default {
    props:{
        backgroundcolor: String,
        color: String
    },
    methods: {
        handleClick(){
            this.$emit("handleClick");
        }
    },
};
</script>

<style scoped>
    button{
        border-style: none;
        border-radius: 10px;
        padding: 13px;
        color: #fff;
        font-family: Notosans-Regular;
        font-size: 13px;
        font-weight: 600;
    }
    button:hover{
        cursor: pointer;
        opacity: 0.8;
    }
    button:active{
        opacity: 1;
    }
</style>
