<template>
  <div style="height: 100%;">
    <h1 style="text-align: right; margin-bottom: 50px;">Devora Lime Arena</h1>
    <el-row justify="end">
      <el-col :span="2">
        <el-button type="primary" @click="state.dialogVisible = true">Add new</el-button>
      </el-col>
    </el-row>
    <el-row class="arena-list-header">
      <el-col :span="1">Id</el-col>
      <el-col :span="10">Arena Name</el-col>
      <el-col :span="4">No. of heroes</el-col>
      <el-col :span="4">Winner</el-col>
    </el-row>
    <el-row v-for="data in state.items" class="arena-list-row">
      <el-row>
        <el-col :span="1">{{ data.id }}</el-col>
        <el-col :span="9">{{ data.name }}</el-col>
        <el-col :span="4">{{ data.numberOfHeroes }}</el-col>
        <el-col :span="5">
          {{ data.winner ? data.winner : 'Waiting for tournament start'}}
        </el-col>
        <el-col :span="5" v-if="data.logs.length === 0">
          <el-button @click="onFight(data.id)">FIGHT</el-button>
        </el-col>
      </el-row>
      <el-collapse v-model="activeNames" @change="handleChange">
        <el-collapse-item title="Logs" name="1">
          <div v-for="log in data.logs">
            <p>{{ log }}</p>
          </div>
        </el-collapse-item>
      </el-collapse>
    </el-row>
    <el-pagination small background layout="prev, pager, next" :total="state.count" :default-page-size="10"
      class="pagination" @change="onPageChange" />
  </div>

  <el-dialog v-model="state.dialogVisible" title="Add new arena with hero" width="500" :before-close="handleClose">
    <template #footer>
      <div class="dialog-footer">
        <el-row>
          <el-input-number v-model="numberOfHeros" :min="1" :max="100" />
        </el-row>
        <el-row justify="end">
          <el-button @click="state.dialogVisible = false">Cancel</el-button>
          <el-button type="primary" @click="onAddNewArena()">Confirm</el-button>
        </el-row>
      </div>
    </template>
  </el-dialog>

  <el-dialog v-model="state.logDialogVisible" title="Fight Logs" width="500" :before-close="handleClose">
    <template #footer>
      <div class="dialog-footer">
        <el-row v-for="fightLog in fightLogs">
          <p>{{ fightLog }}</p>
        </el-row>
        <el-row justify="end">
          <el-button @click="onFightLogClose()">OK</el-button>
        </el-row>
      </div>
    </template>
  </el-dialog>
</template>

<script setup>
import { ElNotification } from 'element-plus';
import { httpClient } from '../../infrastructure/httpClient';

const router = useRouter();
const state = reactive({});
const fightLogs = reactive({});
let numberOfHeros = ref(1);

const onAddNewArena = async () => {
  const body = {
    numberOfHeros: numberOfHeros?.value ?? 1
  }
  const response = await httpClient.postAsync('api/hero/add', body, false, true);

  if (response.success && response.data.arenaId > 0) {
    ElNotification({
      title: 'Success',
      message: 'Arena with heroes created',
      type: 'success',
      duration: 2000
    });

    state.dialogVisible = !state.dialogVisible;
    numberOfHeros = ref(1);
    await loadData();
  }
  else {
    ElNotification({
      title: 'Error',
      message: response.data.exception,
      type: 'error',
      duration: 0
    });
  }
}

const onPageChange = async (nextPage) => {
  state.page = nextPage;
  await loadData()
}

const onFight = async (arenaId) => {
  store.state.loading = true;

  const response = await httpClient.getAsync(`api/arena/fight/${arenaId}`, null, false);
  httpClient.clearCache(`api/arena/fight/${arenaId}`);

  store.state.loading = false;

  if (response.success) {
    Object.assign(fightLogs, response.data.log);
    state.logDialogVisible = true;
  }
  else {
    ElNotification({
      title: 'Error',
      message: response.data.exception,
      type: 'error',
      duration: 0
    });
  }
}

const onFightLogClose = async () => {
  state.logDialogVisible = false;
  Object.assign(fightLogs, {});

  await loadData();
}

const loadData = async () => {
  const page = state?.page ?? 1;
  const response = await httpClient.getAsync(`api/arena/page/${page}`);
  httpClient.clearCache(`api/arena/page/${page}`);

  if (response.success) {
    Object.assign(state, {
      ...response.data,
      page: 1,
      dialogVisible: false,
      logDialogVisible: false
    });
    store.state.loading = false;
  }
  else {
    ElNotification({
      title: 'Error',
      message: response.data.exception,
      type: 'error',
      duration: 0
    });
  }
}

onBeforeMount(async () => {
  await loadData();
});

</script>