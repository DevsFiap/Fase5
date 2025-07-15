# Fase5 - FastTech Foods – Plataforma de Pedidos (Hackathon)

MVP desenvolvido pelos alunos do curso Arquitetura de Sistemas .NET 
Video de apresentação do projeto: https://drive.google.com/file/d/1EXbrf8zYpJwqpO00MRMh__sAVgHFMOPN/view?usp=sharing

---

## Índice

1. [Visão Geral](#visão-geral)
2. [Arquitetura da Solução](#arquitetura-da-solução)
3. [Requisitos Funcionais](#requisitos-funcionais)
4. [Stack Tecnológica](#stack-tecnológica)
5. [Infraestrutura & Observabilidade](#infraestrutura--observabilidade)
---

## Visão Geral

A **FastTech Foods** está construindo uma plataforma própria de atendimento e pedidos, visando **escalabilidade, segurança, observabilidade e automação**. Este repositório contém o MVP entregue no hackathon, abrangendo microsserviços em .NET 8, mensageria com RabbitMQ, orquestração via Kubernetes e monitoramento com Zabbix/Grafana.

---

## Arquitetura da Solução
<img width="1194" height="741" alt="Desenho da arquitetura" src="https://github.com/user-attachments/assets/229ca58a-c86c-40f7-b35b-f56a3a967d89" />


| Microsserviço              | Linguagem | Responsabilidade                                       | Banco      |
| -------------------------- | --------- | ------------------------------------------------------ | ---------- |
| **api-service**            | .NET 8    | Cadastro/consulta de clientes & pedidos / Autenticação | SQL Server |
| **funcionarios-service**   | .NET 8    | Gestão de cardápio e aceite/rejeição de pedidos        | SQL Server |

**Mensageria**: RabbitMQ gerencia dois tópicos principais:
- `fila-cadastrar-pedido` – criação/atualização de pedidos.

**Observabilidade**: métrica/log → Zabbix (coleta) → Grafana (dashboards).

**Infra Cloud**: imagens Docker em **Azure Container Registry (ACR)**, pods rodando em **Kubernetes** (desenvolvimento local via Docker Desktop; produção via AKS). RabbitMQ, Zabbix e Grafana podem rodar em **Azure Container Instances (ACI)** para simplificar o provisionamento.

---

## Requisitos Funcionais

- **Autenticação do Funcionário** (e‑mail corporativo + senha)
- **Cadastro/Edição de Cardápio** (gerente)
- **Aceite ou Rejeição de Pedidos** (cozinha)
- **Autenticação do Cliente** (CPF ou e‑mail + senha)
- **Busca por Produtos** com filtros (tipo de refeição)
- **Realização de Pedido** (balcão, drive‑thru ou delivery) com opção de cancelamento antes do preparo

---

## Stack Tecnológica

| Camada              | Tecnologia                   |
| ------------------- | ---------------------------- |
| Linguagem & Runtime | **.NET 8**                   |
| Mensageria          | **RabbitMQ**                 |
| Banco de Dados      | **SQL Server 2022**          |
| Containerização     | **Docker**                   |
| Orquestração        | **Kubernetes** (local/AKS)   |
| Cloud Registry      | **Azure Container Registry** |
| Observabilidade     | **Zabbix + Grafana**         |
| CI/CD               | **GitHub Actions**           |

---

## Infraestrutura & Observabilidade

- **Deployments & ReplicaSets** – garantem disponibilidade dos microsserviços.
- **Services (ClusterIP/LoadBalancer)** – expõem APIs interna ou externamente.
- **ConfigMaps/Secrets** – connection‑strings, JWT keys, etc.
- **PersistentVolumes/Claims** – dados do SQL Server & RabbitMQ.

**Zabbix → Grafana**:

1. Zabbix Server coleta métricas (pod status, CPU, memória, logs).
2. Datasource *Zabbix* no Grafana exibe dashboards prontos para DevOps.

   <img width="1118" height="618" alt="image" src="https://github.com/user-attachments/assets/231cc2d6-68d2-4cbb-bad0-e7acc6f157cb" />
