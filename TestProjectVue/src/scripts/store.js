import { createStore } from 'vuex'

const store = createStore({
  state: {
    properties: {
      recclasses: []
    },
    cometsReport: null,
    error: null
  },
  getters: {
    properties: state => {
      return state.properties;
    },
    cometsReport: state => {
      return state.cometsReport;
    }
  },
  mutations: {  
    addProperties(state, res) {
      state.properties = res;
    },
    addCometsReport(state, res) {
      state.cometsReport = res.list;
    },
    clearData(state) {
      state.properties = {
        recclasses: []
      };
      state.cometsReport = null
    },
  },
  actions: {
    onLoadComets() {
      return defaultRequest("GET", "Comet/LoadComets", null);
    },
    async onGetCometsByYear(context, data) {
      let res = await defaultRequest("POST", "Comet/GetCometsByYear", data);
      context.commit('addCometsReport', res);
    },
    async onGetProperties(context) {
      let res = await defaultRequest("GET", "Comet/GetProperties", null);
      context.commit('addProperties', res);
    },
    async onClearDB(context) {
      await defaultRequest("GET", "Comet/ClearDB", null)
      context.commit('clearData');
    },
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
    throw error;
  }
}




