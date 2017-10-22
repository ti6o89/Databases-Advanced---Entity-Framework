USE MinionsDB

SELECT v.Name,
COUNT(mv.MinionId) AS MinionsCount
FROM Villains AS v
JOIN MinionsVillains AS mv ON mv.VillainId = v.Id
GROUP BY v.Name
HAVING COUNT(mv.MinionId) > 3
ORDER BY MinionsCount DESC