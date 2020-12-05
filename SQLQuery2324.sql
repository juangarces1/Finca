UPDATE
    Tabla_A
SET
    Tabla_A.IsPrenez = Tabla_B.Estado

FROM
    Animals AS Tabla_A
    INNER JOIN Palpation AS Tabla_B
        ON Tabla_A.id = Tabla_B.AnimalId
        where Tabla_A.NumeroFinca > '0'
