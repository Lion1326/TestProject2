import { createStore } from 'vuex'

const store = createStore({
  state: {
    properties: {
      recclasses: [],
      years:[]
    },
    cometsReport: null,
    error: null,
    loading: false
  },
  getters: {
    properties: state => {
      return state.properties;
    },
    cometsReport: state => {
      return state.cometsReport;
    },
    loading: state => {
      return state.loading;
    },
    error: state => {
      return state.error;
    },
  },
  mutations: {  
    addProperties(state, res) {
      state.properties = res;
    },
    addCometsReport(state, res) {
      state.cometsReport = res;
    },
    clearData(state) {
      state.properties = {
        recclasses: []
      };
      state.cometsReport = null;
    },
    setLoading(state,val){
      state.loading = val;
    }
  },
  actions: {
    async onLoadComets(context) {
      context.dispatch("showLoader");
      let result =await defaultRequest("GET", "Comet/LoadComets", null);
      context.dispatch("hideLoader");
      return result;
    },
    async onGetCometsByYear(context, data) {
      context.dispatch("showLoader");
      let res = await defaultRequest("POST", "Comet/GetCometsByYear", data);
      context.commit('addCometsReport', res);
      context.dispatch("hideLoader");
    },
    async onGetProperties(context) {
      context.dispatch("showLoader");
      let res = await defaultRequest("GET", "Comet/GetProperties", null);
      context.commit('addProperties', res);
      context.dispatch("hideLoader");
    },
    async onClearDB(context) {
      context.dispatch("showLoader");
      await defaultRequest("GET", "Comet/ClearDB", null);
      context.commit('clearData');
      context.dispatch("hideLoader");
    },
    showLoader(context){
      context.commit('setLoading',true);
    },
    hideLoader(context){
      context.commit('setLoading',false);
    }
  }
})


export default store

async function defaultRequest(method, path, body) {
  try {
    let res = await fetch(process.env.VUE_APP_API_URL + "/" + path, {
      method: method,
      headers: {
        'Content-Type': 'application/json; charset=utf-8'
      },
      body: body ? JSON.stringify(body) : null
    })
    if (res.ok) {
      if (res.headers.get("content-type") == "application/json; charset=utf-8")
        return res.json();
      else return res;
    }
    throw res;
  }
  catch (error) {
    store.state.error = error;
    store.dispatch('hideLoader');
    throw error;
  }
}




