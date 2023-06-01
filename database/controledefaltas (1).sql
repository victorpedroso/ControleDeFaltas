-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 31-Maio-2023 às 22:31
-- Versão do servidor: 10.4.27-MariaDB
-- versão do PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `controledefaltas`
--
CREATE DATABASE IF NOT EXISTS `controledefaltas` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `controledefaltas`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `alunos`
--

CREATE TABLE `alunos` (
  `id` int(11) NOT NULL,
  `nome` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `matricula` int(11) DEFAULT NULL,
  `curso` int(11) NOT NULL,
  `turma` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `alunos`
--

INSERT INTO `alunos` (`id`, `nome`, `email`, `matricula`, `curso`, `turma`) VALUES
(5, 'victor pedroso', 'victor1pedroso@gmail.com', 1234, 7, 2),
(6, 'helio aluno', 'helio@teste.com', 123641, 7, 2),
(7, 'daniel', 'daniel@teste.com', 123545454, 7, 2),
(8, 'teste', 'teste@11', 1111, 8, NULL),
(9, 'victor', 'victor@victor.com', 1234723843, 10, NULL),
(10, 'testecurso', 'vvvvvvvvvvvvvv', 2147483647, 9, NULL),
(11, 'Carina', 'carina@gmail.com', 1234444, 9, NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `alunosmatriculados`
--

CREATE TABLE `alunosmatriculados` (
  `id` int(11) NOT NULL,
  `aluno_matricula` int(11) DEFAULT NULL,
  `disciplina_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `alunosmatriculados`
--

INSERT INTO `alunosmatriculados` (`id`, `aluno_matricula`, `disciplina_id`) VALUES
(1, 5, 11),
(2, 6, 14),
(3, 7, 14);

-- --------------------------------------------------------

--
-- Estrutura da tabela `cursos`
--

CREATE TABLE `cursos` (
  `id` int(11) NOT NULL,
  `nome` varchar(255) NOT NULL,
  `qtdeMatriculados` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `cursos`
--

INSERT INTO `cursos` (`id`, `nome`, `qtdeMatriculados`) VALUES
(7, 'engenharia', NULL),
(8, 'biologia', NULL),
(9, 'letras', NULL),
(10, 'ciencias', NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `disciplinas`
--

CREATE TABLE `disciplinas` (
  `id` int(11) NOT NULL,
  `nome` varchar(255) DEFAULT NULL,
  `responsavel` int(11) DEFAULT NULL,
  `horario` varchar(100) DEFAULT NULL,
  `curso` int(11) DEFAULT NULL,
  `qtdeHoras` varchar(100) DEFAULT NULL,
  `qtdeMatriculados` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `disciplinas`
--

INSERT INTO `disciplinas` (`id`, `nome`, `responsavel`, `horario`, `curso`, `qtdeHoras`, `qtdeMatriculados`) VALUES
(7, 'teste', 5, NULL, 8, '0', NULL),
(8, 'teste2', 5, NULL, 8, '1', NULL),
(9, 'teste3', 5, NULL, 8, '2', NULL),
(10, 'teste5', 1, NULL, 9, '2', NULL),
(11, 'testeee', 6, NULL, 7, '4', NULL),
(12, 'testeeee', 5, NULL, 7, '4', NULL),
(13, 'testando', 6, NULL, 9, '4', NULL),
(14, 'testando 2', 6, NULL, 8, '2', NULL),
(15, 'teste disc', 4, NULL, 9, '2', NULL),
(16, 'anatomia', 5, NULL, 8, '4', NULL);

-- --------------------------------------------------------

--
-- Estrutura da tabela `faltas`
--

CREATE TABLE `faltas` (
  `id` int(11) NOT NULL,
  `aluno_id` int(11) NOT NULL,
  `disciplina_id` int(11) NOT NULL,
  `dataFalta` date NOT NULL,
  `qtdeFaltas` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `faltas`
--

INSERT INTO `faltas` (`id`, `aluno_id`, `disciplina_id`, `dataFalta`, `qtdeFaltas`) VALUES
(2, 5, 11, '0000-00-00', 2),
(3, 6, 14, '2022-04-05', 2),
(4, 7, 14, '2022-04-05', 2),
(5, 5, 11, '0000-00-00', 3),
(6, 5, 11, '2022-04-12', 6),
(7, 5, 11, '2023-05-12', 0),
(8, 5, 11, '2023-05-16', 0),
(9, 5, 11, '2023-05-14', 0),
(10, 5, 11, '2023-05-12', 3),
(11, 5, 7, '2022-04-11', 4),
(12, 5, 11, '2023-05-21', 3),
(13, 5, 11, '2023-05-21', 4),
(14, 5, 11, '2023-05-21', 0),
(15, 5, 11, '2023-05-22', 0);

-- --------------------------------------------------------

--
-- Estrutura da tabela `logacesso`
--

CREATE TABLE `logacesso` (
  `id` int(11) NOT NULL,
  `nome_usuario` varchar(255) NOT NULL,
  `nivelUsuario` int(11) NOT NULL,
  `horaAcesso` varchar(255) NOT NULL,
  `tipoAcesso` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `logacesso`
--

INSERT INTO `logacesso` (`id`, `nome_usuario`, `nivelUsuario`, `horaAcesso`, `tipoAcesso`) VALUES
(1, 'victor professor teste', 0, '12/05/2023 16:53:21', 'ENTRADA'),
(2, 'victor professor teste', 0, '12/05/2023 16:53:26', 'SAIDA'),
(3, 'victor professor teste', 0, '12/05/2023 19:32:19', 'ENTRADA'),
(4, 'victor professor teste', 0, '12/05/2023 19:36:29', 'SAIDA'),
(5, 'victor professor teste', 0, '12/05/2023 20:45:36', 'ENTRADA'),
(6, 'victor professor teste', 0, '12/05/2023 21:00:57', 'ENTRADA'),
(7, 'victor professor teste', 0, '12/05/2023 21:01:11', 'SAIDA'),
(8, 'victor professor teste', 0, '12/05/2023 21:01:54', 'ENTRADA'),
(9, 'victor professor teste', 0, '12/05/2023 21:08:45', 'SAIDA'),
(10, 'victor professor teste', 0, '12/05/2023 21:28:02', 'ENTRADA'),
(11, 'victor professor teste', 0, '12/05/2023 21:28:17', 'SAIDA'),
(12, 'victor professor teste', 0, '12/05/2023 21:29:54', 'ENTRADA'),
(13, 'victor professor teste', 0, '12/05/2023 21:30:09', 'SAIDA'),
(14, 'victor professor teste', 0, '12/05/2023 21:30:55', 'ENTRADA'),
(15, 'victor professor teste', 0, '12/05/2023 21:31:22', 'SAIDA'),
(16, 'victor professor teste', 0, '12/05/2023 21:32:32', 'ENTRADA'),
(17, 'victor professor teste', 0, '12/05/2023 21:32:49', 'SAIDA'),
(18, 'victor professor teste', 0, '12/05/2023 21:40:14', 'ENTRADA'),
(19, 'victor professor teste', 0, '12/05/2023 21:40:22', 'SAIDA'),
(20, 'victor professor teste', 0, '12/05/2023 21:41:30', 'ENTRADA'),
(21, 'victor professor teste', 0, '12/05/2023 21:42:19', 'ENTRADA'),
(22, 'victor professor teste', 0, '12/05/2023 21:42:37', 'SAIDA'),
(23, 'victor professor teste', 0, '12/05/2023 21:43:08', 'ENTRADA'),
(24, 'victor professor teste', 0, '12/05/2023 21:43:29', 'SAIDA'),
(25, 'victor professor teste', 0, '12/05/2023 21:43:58', 'ENTRADA'),
(26, 'victor professor teste', 0, '12/05/2023 21:44:31', 'SAIDA'),
(27, 'victor professor teste', 0, '12/05/2023 21:47:54', 'ENTRADA'),
(28, 'victor professor teste', 0, '12/05/2023 21:48:05', 'SAIDA'),
(29, 'victor professor teste', 0, '12/05/2023 21:48:46', 'ENTRADA'),
(30, 'victor professor teste', 0, '12/05/2023 21:49:06', 'SAIDA'),
(31, 'victor professor teste', 0, '12/05/2023 21:49:30', 'ENTRADA'),
(32, 'victor professor teste', 0, '12/05/2023 21:49:39', 'SAIDA'),
(33, 'victor professor teste', 0, '12/05/2023 21:50:01', 'ENTRADA'),
(34, 'victor professor teste', 0, '12/05/2023 21:50:20', 'SAIDA'),
(35, 'victor professor teste', 0, '13/05/2023 12:33:56', 'ENTRADA'),
(36, 'victor professor teste', 0, '13/05/2023 12:34:58', 'SAIDA'),
(37, 'victor professor teste', 0, '13/05/2023 12:36:59', 'ENTRADA'),
(38, 'victor professor teste', 0, '13/05/2023 12:38:58', 'ENTRADA'),
(39, 'victor professor teste', 0, '13/05/2023 12:39:24', 'SAIDA'),
(40, 'victor professor teste', 0, '13/05/2023 12:41:46', 'ENTRADA'),
(41, 'victor professor teste', 0, '13/05/2023 12:42:53', 'SAIDA'),
(42, 'victor professor teste', 0, '13/05/2023 12:43:41', 'ENTRADA'),
(43, 'victor professor teste', 0, '13/05/2023 12:44:15', 'SAIDA'),
(44, 'victor professor teste', 0, '13/05/2023 12:45:55', 'ENTRADA'),
(45, 'victor professor teste', 0, '13/05/2023 12:46:15', 'SAIDA'),
(46, 'victor professor teste', 0, '13/05/2023 12:47:50', 'ENTRADA'),
(47, 'victor professor teste', 0, '13/05/2023 12:52:22', 'ENTRADA'),
(48, 'victor professor teste', 0, '13/05/2023 12:52:29', 'SAIDA'),
(49, 'victor professor teste', 0, '13/05/2023 12:52:53', 'ENTRADA'),
(50, 'victor professor teste', 0, '13/05/2023 12:53:28', 'SAIDA'),
(51, 'victor professor teste', 0, '13/05/2023 12:53:46', 'ENTRADA'),
(52, 'victor professor teste', 0, '13/05/2023 12:55:13', 'SAIDA'),
(53, 'victor professor teste', 0, '16/05/2023 21:01:22', 'ENTRADA'),
(54, 'victor professor teste', 0, '16/05/2023 21:03:37', 'SAIDA'),
(55, 'victor professor teste', 0, '16/05/2023 21:06:01', 'ENTRADA'),
(56, 'victor professor teste', 0, '16/05/2023 21:06:29', 'SAIDA'),
(57, 'victor professor teste', 0, '16/05/2023 21:32:50', 'ENTRADA'),
(58, 'victor professor teste', 0, '16/05/2023 21:32:59', 'SAIDA'),
(59, 'victor professor teste', 0, '16/05/2023 21:33:20', 'ENTRADA'),
(60, 'victor professor teste', 0, '16/05/2023 21:33:53', 'SAIDA'),
(61, 'victor professor teste', 0, '16/05/2023 21:34:30', 'ENTRADA'),
(62, 'victor professor teste', 0, '16/05/2023 21:34:41', 'SAIDA'),
(63, 'victor professor teste', 0, '16/05/2023 21:41:23', 'ENTRADA'),
(64, 'victor professor teste', 0, '16/05/2023 21:42:52', 'ENTRADA'),
(65, 'victor professor teste', 0, '16/05/2023 21:43:19', 'ENTRADA'),
(66, 'victor professor teste', 0, '16/05/2023 21:43:28', 'SAIDA'),
(67, 'victor professor teste', 0, '16/05/2023 21:43:56', 'ENTRADA'),
(68, 'victor professor teste', 0, '16/05/2023 21:44:04', 'SAIDA'),
(69, 'victor professor teste', 0, '19/05/2023 20:26:44', 'ENTRADA'),
(70, 'victor professor teste', 0, '19/05/2023 20:27:46', 'SAIDA'),
(71, 'victor professor teste', 0, '19/05/2023 20:29:53', 'ENTRADA'),
(72, 'victor professor teste', 0, '19/05/2023 20:30:38', 'ENTRADA'),
(73, 'victor professor teste', 0, '19/05/2023 20:33:18', 'ENTRADA'),
(74, 'victor professor teste', 0, '19/05/2023 20:33:28', 'SAIDA'),
(75, 'victor professor teste', 0, '19/05/2023 21:02:17', 'ENTRADA'),
(76, 'victor professor teste', 0, '19/05/2023 21:03:17', 'ENTRADA'),
(77, 'victor professor teste', 0, '19/05/2023 21:05:34', 'SAIDA'),
(78, 'victor professor teste', 0, '19/05/2023 21:11:59', 'ENTRADA'),
(79, 'victor professor teste', 0, '19/05/2023 21:14:43', 'ENTRADA'),
(80, 'victor professor teste', 0, '19/05/2023 21:16:13', 'ENTRADA'),
(81, 'victor professor teste', 0, '19/05/2023 21:17:11', 'ENTRADA'),
(82, 'victor professor teste', 0, '19/05/2023 21:32:56', 'ENTRADA'),
(83, 'victor professor teste', 0, '19/05/2023 21:33:00', 'SAIDA'),
(84, 'victor professor teste', 0, '19/05/2023 21:33:15', 'ENTRADA'),
(85, 'victor professor teste', 0, '19/05/2023 21:33:29', 'SAIDA'),
(86, 'victor professor teste', 0, '19/05/2023 21:34:03', 'ENTRADA'),
(87, 'victor professor teste', 0, '19/05/2023 21:34:15', 'SAIDA'),
(88, 'victor professor teste', 0, '20/05/2023 11:30:09', 'ENTRADA'),
(89, 'victor professor teste', 0, '20/05/2023 11:30:46', 'SAIDA'),
(90, 'victor professor teste', 0, '20/05/2023 11:31:09', 'ENTRADA'),
(91, 'victor professor teste', 0, '20/05/2023 11:31:29', 'SAIDA'),
(92, 'victor professor teste', 0, '20/05/2023 11:32:17', 'ENTRADA'),
(93, 'victor professor teste', 0, '20/05/2023 11:32:31', 'SAIDA'),
(94, 'victor professor teste', 0, '20/05/2023 11:33:05', 'ENTRADA'),
(95, 'victor professor teste', 0, '20/05/2023 11:33:19', 'SAIDA'),
(96, 'victor professor teste', 0, '20/05/2023 11:35:42', 'ENTRADA'),
(97, 'victor professor teste', 0, '20/05/2023 11:35:58', 'SAIDA'),
(98, 'victor professor teste', 0, '20/05/2023 11:37:18', 'ENTRADA'),
(99, 'victor professor teste', 0, '20/05/2023 11:37:34', 'SAIDA'),
(100, 'victor professor teste', 0, '20/05/2023 11:38:17', 'ENTRADA'),
(101, 'victor professor teste', 0, '20/05/2023 11:38:26', 'SAIDA'),
(102, 'victor professor teste', 0, '20/05/2023 11:40:31', 'ENTRADA'),
(103, 'victor professor teste', 0, '20/05/2023 11:43:50', 'ENTRADA'),
(104, 'victor professor teste', 0, '20/05/2023 11:44:17', 'SAIDA'),
(105, 'victor professor teste', 0, '20/05/2023 11:50:59', 'ENTRADA'),
(106, 'victor professor teste', 0, '20/05/2023 11:51:11', 'SAIDA'),
(107, 'victor professor teste', 0, '20/05/2023 11:51:40', 'ENTRADA'),
(108, 'victor professor teste', 0, '20/05/2023 11:51:57', 'SAIDA'),
(109, 'victor professor teste', 0, '20/05/2023 11:53:38', 'ENTRADA'),
(110, 'victor professor teste', 0, '20/05/2023 11:55:23', 'ENTRADA'),
(111, 'victor professor teste', 0, '20/05/2023 11:57:13', 'SAIDA'),
(112, 'victor professor teste', 0, '20/05/2023 12:02:20', 'ENTRADA'),
(113, 'victor professor teste', 0, '20/05/2023 12:03:00', 'SAIDA'),
(114, 'victor professor teste', 0, '20/05/2023 12:04:16', 'ENTRADA'),
(115, 'victor professor teste', 0, '20/05/2023 12:05:51', 'SAIDA'),
(116, 'victor professor teste', 0, '2023-05-20 16:53:44.295540', 'ENTRADA'),
(117, 'victor professor teste', 0, '2023-05-20 16:54:14.637025', 'ENTRADA'),
(118, 'victor professor teste', 0, '2023-05-20 16:55:45.975817', 'ENTRADA'),
(119, 'victor professor teste', 0, '2023-05-20 17:41:11.317810', 'ENTRADA'),
(120, 'victor professor teste', 0, '2023-05-20 17:42:22.365022', 'ENTRADA'),
(121, 'victor professor teste', 0, '2023-05-20 17:45:16.511827', 'ENTRADA'),
(122, 'victor professor teste', 0, '2023-05-20 17:46:30.234770', 'ENTRADA'),
(123, 'victor professor teste', 0, '2023-05-20 17:48:24.871634', 'ENTRADA'),
(124, 'victor professor teste', 0, '2023-05-20 17:50:42.677563', 'ENTRADA'),
(125, 'victor professor teste', 0, '2023-05-20 17:52:03.056982', 'ENTRADA'),
(126, 'victor professor teste', 0, '2023-05-20 17:59:02.662651', 'ENTRADA'),
(127, 'victor professor teste', 0, '2023-05-20 17:59:32.536157', 'ENTRADA'),
(128, 'victor professor teste', 0, '2023-05-20 17:59:55.742892', 'ENTRADA'),
(129, 'victor professor teste', 0, '2023-05-20 18:00:51.653298', 'ENTRADA'),
(130, 'victor professor teste', 0, '2023-05-20 18:01:25.346711', 'ENTRADA'),
(131, 'victor professor teste', 0, '2023-05-20 18:04:56.366665', 'ENTRADA'),
(132, 'victor professor teste', 0, '2023-05-21 10:35:08.694249', 'ENTRADA'),
(133, 'victor professor teste', 0, '2023-05-21 10:39:57.317525', 'ENTRADA'),
(134, 'victor professor teste', 0, '2023-05-21 10:40:19.122070', 'ENTRADA'),
(135, 'victor professor teste', 0, '2023-05-21 10:51:45.312064', 'ENTRADA'),
(136, 'victor professor teste', 0, '2023-05-21 10:58:54.054580', 'ENTRADA'),
(137, 'victor professor teste', 0, '2023-05-21 10:59:52.357715', 'ENTRADA'),
(138, 'victor professor teste', 0, '2023-05-21 11:00:48.394013', 'ENTRADA'),
(139, 'victor professor teste', 0, '2023-05-21 11:04:22.043817', 'ENTRADA'),
(140, 'victor professor teste', 0, '2023-05-21 11:07:55.645112', 'ENTRADA'),
(141, 'victor professor teste', 0, '2023-05-21 11:08:42.518317', 'ENTRADA'),
(142, 'victor professor teste', 0, '2023-05-21 11:13:43.761697', 'ENTRADA'),
(143, 'victor professor teste', 0, '2023-05-21 11:14:02.642267', 'ENTRADA'),
(144, 'victor professor teste', 0, '2023-05-21 11:14:56.378914', 'ENTRADA'),
(145, 'victor professor teste', 0, '2023-05-21 11:18:50.034907', 'ENTRADA'),
(146, 'victor professor teste', 0, '2023-05-21 11:20:31.330108', 'ENTRADA'),
(147, 'victor professor teste', 0, '2023-05-21 11:24:11.841022', 'ENTRADA'),
(148, 'victor professor teste', 0, '2023-05-21 11:26:18.579032', 'ENTRADA'),
(149, 'victor professor teste', 0, '2023-05-21 11:29:12.221034', 'ENTRADA'),
(150, 'victor professor teste', 0, '2023-05-21 11:32:08.641582', 'ENTRADA'),
(151, 'victor professor teste', 0, '2023-05-21 11:33:42.281503', 'ENTRADA'),
(152, 'victor professor teste', 0, '2023-05-21 11:43:27.898016', 'ENTRADA'),
(153, 'victor professor teste', 0, '2023-05-21 11:46:59.175464', 'ENTRADA'),
(154, 'victor professor teste', 0, '2023-05-21 11:48:27.002783', 'ENTRADA'),
(155, 'victor professor teste', 0, '2023-05-21 11:49:21.543577', 'ENTRADA'),
(156, 'victor professor teste', 0, '2023-05-21 11:53:16.940308', 'ENTRADA'),
(157, 'victor professor teste', 0, '2023-05-21 11:53:48.433108', 'ENTRADA'),
(158, 'victor professor teste', 0, '2023-05-21 11:56:08.581644', 'ENTRADA'),
(159, 'victor professor teste', 0, '2023-05-21 12:00:54.258813', 'ENTRADA'),
(160, '', 0, '2023-05-21 12:04:05.986210', 'ENTRADA'),
(161, 'victor professor teste', 0, '2023-05-25 19:05:28.466662', 'ENTRADA'),
(162, 'professor 1', 0, '2023-05-31 17:28:46.008121', 'ENTRADA'),
(163, 'professor 1', 0, '2023-05-31 17:30:02.899659', 'ENTRADA');

-- --------------------------------------------------------

--
-- Estrutura da tabela `professor`
--

CREATE TABLE `professor` (
  `id` int(11) NOT NULL,
  `nome` varchar(255) NOT NULL,
  `email` varchar(200) NOT NULL,
  `senha` varchar(255) NOT NULL,
  `registro` int(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `professor`
--

INSERT INTO `professor` (`id`, `nome`, `email`, `senha`, `registro`) VALUES
(1, 'victor', 'victor@gmail.com', '123', 12345),
(2, 'victor', 'victor@gmail.com', '1234', 12345),
(3, 'victor', 'victor@gmail.com1', '123', 12345),
(4, 'victor prof teste', 'teste@gmail.com', '123456', 12309847),
(5, 'helio', 'helio@gmail.com', '1234', 4321),
(6, 'professor 1', 'prof@prof.com', '123', 123475),
(8, 'Carina', ' carina@gmail.com', ' teste', 4444444);

-- --------------------------------------------------------

--
-- Estrutura da tabela `turmas`
--

CREATE TABLE `turmas` (
  `id` int(11) NOT NULL,
  `nome` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `turmas`
--

INSERT INTO `turmas` (`id`, `nome`) VALUES
(2, 'turma1'),
(3, 'turma2');

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `nome` varchar(255) NOT NULL,
  `email` varchar(200) NOT NULL,
  `senha` varchar(255) NOT NULL,
  `nivelAcesso` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `usuarios`
--

INSERT INTO `usuarios` (`id`, `nome`, `email`, `senha`, `nivelAcesso`) VALUES
(1, 'victor professor teste', 'prof@prof.com', '123', 0),
(2, 'admin', 'admin@admin.com', '123', 1);

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `alunos`
--
ALTER TABLE `alunos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_curso` (`curso`),
  ADD KEY `turma` (`turma`);

--
-- Índices para tabela `alunosmatriculados`
--
ALTER TABLE `alunosmatriculados`
  ADD PRIMARY KEY (`id`),
  ADD KEY `matricula` (`aluno_matricula`),
  ADD KEY `disciplina` (`disciplina_id`);

--
-- Índices para tabela `cursos`
--
ALTER TABLE `cursos`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `disciplinas`
--
ALTER TABLE `disciplinas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_responsavel` (`responsavel`),
  ADD KEY `fk_curso_disciplina` (`curso`);

--
-- Índices para tabela `faltas`
--
ALTER TABLE `faltas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_alunoId` (`aluno_id`),
  ADD KEY `fk_DisciplinaId` (`disciplina_id`);

--
-- Índices para tabela `logacesso`
--
ALTER TABLE `logacesso`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `professor`
--
ALTER TABLE `professor`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `turmas`
--
ALTER TABLE `turmas`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `alunos`
--
ALTER TABLE `alunos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de tabela `alunosmatriculados`
--
ALTER TABLE `alunosmatriculados`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `cursos`
--
ALTER TABLE `cursos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de tabela `disciplinas`
--
ALTER TABLE `disciplinas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de tabela `faltas`
--
ALTER TABLE `faltas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de tabela `logacesso`
--
ALTER TABLE `logacesso`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=164;

--
-- AUTO_INCREMENT de tabela `professor`
--
ALTER TABLE `professor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de tabela `turmas`
--
ALTER TABLE `turmas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `alunos`
--
ALTER TABLE `alunos`
  ADD CONSTRAINT `alunos_ibfk_1` FOREIGN KEY (`turma`) REFERENCES `turmas` (`id`),
  ADD CONSTRAINT `fk_curso` FOREIGN KEY (`curso`) REFERENCES `cursos` (`id`);

--
-- Limitadores para a tabela `alunosmatriculados`
--
ALTER TABLE `alunosmatriculados`
  ADD CONSTRAINT `disciplina` FOREIGN KEY (`disciplina_id`) REFERENCES `disciplinas` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `matricula` FOREIGN KEY (`aluno_matricula`) REFERENCES `alunos` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `disciplinas`
--
ALTER TABLE `disciplinas`
  ADD CONSTRAINT `fk_curso_disciplina` FOREIGN KEY (`curso`) REFERENCES `cursos` (`id`),
  ADD CONSTRAINT `fk_responsavel` FOREIGN KEY (`responsavel`) REFERENCES `professor` (`id`);

--
-- Limitadores para a tabela `faltas`
--
ALTER TABLE `faltas`
  ADD CONSTRAINT `fk_DisciplinaId` FOREIGN KEY (`disciplina_id`) REFERENCES `disciplinas` (`id`),
  ADD CONSTRAINT `fk_alunoId` FOREIGN KEY (`aluno_id`) REFERENCES `alunos` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
