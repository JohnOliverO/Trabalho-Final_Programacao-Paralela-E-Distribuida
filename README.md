# Sistema Bancário TCP em C#

Projeto desenvolvido para a disciplina de **Programação Paralela e Distribuída**.

O sistema implementa um servidor bancário utilizando comunicação TCP, múltiplas threads e mecanismos de sincronização para garantir acesso seguro aos dados compartilhados.

---

# Objetivos

Este projeto tem como objetivo demonstrar os principais conceitos estudados na disciplina, incluindo:

- Programação Concorrente
- Programação Distribuída
- Comunicação Cliente/Servidor
- Sockets TCP
- Threads (Task)
- Exclusão Mútua
- Condição de Corrida
- Atomicidade
- Sincronização com `lock`

---

# Tecnologias

- C#
- .NET 10
- Visual Studio Code
- TCP/IP
- Console Application

---

# Estrutura do Projeto

```
Trabalho-Final
│
├── Cliente
│   └── Program.cs
│
└── Servidor
    ├── Banco.cs
    ├── ClienteHandler.cs
    ├── Comandos.cs
    ├── Conta.cs
    └── Program.cs
```

---

# Arquitetura

```
            +----------------+
            |    Cliente 1   |
            +----------------+
                    |
                    |
                    |
            +----------------+
            |                |
            |    Servidor    |
            |                |
            +----------------+
              /      |      \
             /       |       \
            /        |        \
 +-------------+ +-------------+ +-------------+
 | Cliente 2   | | Cliente 3   | | Cliente N   |
 +-------------+ +-------------+ +-------------+

                 |
                 |
          Objeto Banco
        (Recurso Compartilhado)
```

Cada cliente conectado é atendido por uma **Task** diferente.

Todas as operações bancárias utilizam o mesmo objeto `Banco`, compartilhado entre todas as threads.

---

# Funcionalidades

Consultar saldo

```
saldo 1001
```

Depositar

```
depositar 1001 200
```

Sacar

```
sacar 1001 100
```

Transferir

```
transferir 1001 1002 300
```

Ajuda

```
help
```

---

# Contas disponíveis

| Conta | Saldo Inicial |
|--------|---------------|
|1001|R$ 1000,00|
|1002|R$ 500,00|
|1003|R$ 2500,00|

---

# Como executar

## Servidor

```bash
cd Servidor

dotnet run
```

---

## Cliente

Em outro terminal:

```bash
cd Cliente

dotnet run
```

É possível abrir diversos clientes simultaneamente.

---

# Concorrência

Cada cliente é executado em uma Task criada pelo servidor.

```csharp
Task.Run(() =>
{
    handler.Atender();
});
```

Dessa forma o servidor continua aceitando novas conexões enquanto outros clientes permanecem conectados.

---

# Exclusão Mútua

As operações críticas utilizam `lock`.

```csharp
lock (saldoLock)
{
    ...
}
```

Isso garante que apenas uma thread altere os saldos por vez.

---

# Condição de Corrida

Durante os testes foi utilizado:

```csharp
Thread.Sleep(500);
```

para aumentar a probabilidade de duas threads acessarem o mesmo recurso simultaneamente.

Removendo o `lock`, é possível observar inconsistências nos saldos.

---

# Atomicidade

As operações de:

- saque
- depósito
- transferência

são executadas integralmente dentro da região crítica.

Isso impede alterações parciais do estado do sistema.

---

# Justificativas Técnicas

### Por que Console?

O foco do trabalho é programação paralela e distribuída, e não desenvolvimento de interfaces gráficas.

---

### Por que TCP?

TCP garante entrega confiável das mensagens, característica importante para operações bancárias.

---

### Por que Task?

Permite atender múltiplos clientes simultaneamente sem bloquear o servidor.

---

### Por que lock?

Evita condições de corrida durante operações sobre recursos compartilhados.

---

### Por que não usar async/await?

A utilização de `Task.Run()` torna os conceitos de concorrência e sincronização mais explícitos, atendendo melhor aos objetivos da disciplina.

---

# Autor

John Silva

Disciplina:
Programação Paralela e Distribuída

Curso:
Ciência da Computação