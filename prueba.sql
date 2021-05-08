-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 08-05-2021 a las 21:35:05
-- Versión del servidor: 10.4.11-MariaDB
-- Versión de PHP: 7.2.31

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `prueba`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `getDistributor` (IN `id` VARCHAR(100))  BEGIN
    SELECT CONCAT(nombre, ' ', apellido_paterno, ' ', apellido_materno) AS 'Nombre Completo', 
    calle AS Calle, numero AS Número, colonia AS Colonia FROM distributors 
    INNER JOIN addresses on addresses.id_address = distributors.id_address 
    INNER JOIN persons on persons.id_person = distributors.id_person
    WHERE id_distributor = id;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `addresses`
--

CREATE TABLE `addresses` (
  `id_address` bigint(20) NOT NULL,
  `calle` varchar(100) NOT NULL,
  `numero` int(11) NOT NULL,
  `colonia` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `addresses`
--

INSERT INTO `addresses` (`id_address`, `calle`, `numero`, `colonia`) VALUES
(1, '20 de noviembre', 25, 'Centro'),
(2, 'Luis Alberto', 2, 'Info. Playas');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `distributors`
--

CREATE TABLE `distributors` (
  `id_distributor` varchar(100) NOT NULL,
  `fecha_registro` date NOT NULL,
  `id_person` bigint(20) NOT NULL,
  `id_address` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `distributors`
--

INSERT INTO `distributors` (`id_distributor`, `fecha_registro`, `id_person`, `id_address`) VALUES
('1A', '2021-05-08', 1, 1),
('1b', '2021-05-08', 2, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `persons`
--

CREATE TABLE `persons` (
  `id_person` bigint(20) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido_paterno` varchar(100) NOT NULL,
  `apellido_materno` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `persons`
--

INSERT INTO `persons` (`id_person`, `nombre`, `apellido_paterno`, `apellido_materno`) VALUES
(1, 'Juan', 'Lopez', 'Garcia'),
(2, 'Maria Luisa', 'Sanchez', 'Lopez');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `addresses`
--
ALTER TABLE `addresses`
  ADD PRIMARY KEY (`id_address`);

--
-- Indices de la tabla `distributors`
--
ALTER TABLE `distributors`
  ADD UNIQUE KEY `id_distributor` (`id_distributor`),
  ADD KEY `fk_id_person` (`id_person`),
  ADD KEY `fk_id_address` (`id_address`);

--
-- Indices de la tabla `persons`
--
ALTER TABLE `persons`
  ADD PRIMARY KEY (`id_person`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `addresses`
--
ALTER TABLE `addresses`
  MODIFY `id_address` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `persons`
--
ALTER TABLE `persons`
  MODIFY `id_person` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `distributors`
--
ALTER TABLE `distributors`
  ADD CONSTRAINT `fk_id_address` FOREIGN KEY (`id_address`) REFERENCES `addresses` (`id_address`),
  ADD CONSTRAINT `fk_id_person` FOREIGN KEY (`id_person`) REFERENCES `persons` (`id_person`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
